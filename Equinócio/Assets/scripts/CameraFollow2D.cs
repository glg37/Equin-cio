using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Alvo")]
    public Transform target;

    [Header("Suavização")]
    [SerializeField] private float smoothTime = 0.15f; // menor = mais “colada”
    private Vector3 velocity = Vector3.zero;

    [Header("Offset (Z negativo em 2D)")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Limites opcionais (BoxCollider2D do mapa)")]
    public BoxCollider2D mapBounds;

    private Camera cam;
    private float camHalfHeight;
    private float camHalfWidth;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic == false) cam.orthographic = true; // Top-down 2D
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Suavização de posição
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, 0f) + offset;
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        // Clamping pelos limites do mapa (se fornecido)
        if (mapBounds != null)
        {
            // Metade da câmera (em unidades do mundo)
            camHalfHeight = cam.orthographicSize;
            camHalfWidth = camHalfHeight * cam.aspect;

            Bounds b = mapBounds.bounds;
            float minX = b.min.x + camHalfWidth;
            float maxX = b.max.x - camHalfWidth;
            float minY = b.min.y + camHalfHeight;
            float maxY = b.max.y - camHalfHeight;

            smoothPos.x = Mathf.Clamp(smoothPos.x, minX, maxX);
            smoothPos.y = Mathf.Clamp(smoothPos.y, minY, maxY);
        }

        transform.position = smoothPos;
    }
}
