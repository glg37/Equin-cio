using UnityEngine;

public class TreeTransparency : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [Range(0f, 1f)] public float transparency = 0.5f; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Color newColor = originalColor;
            newColor.a = transparency;
            spriteRenderer.color = newColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.color = originalColor;
        }
    }
}
