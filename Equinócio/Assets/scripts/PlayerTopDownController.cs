using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTopDownController : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 30f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 input;
    private Vector2 desiredVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        input = new Vector2(x, y).normalized;

        desiredVelocity = input * moveSpeed;

        // Animação
        bool isMoving = input.sqrMagnitude > 0.01f;
        animator.SetBool("isMoving", isMoving);

        if (x > 0.01f)
            spriteRenderer.flipX = true; // indo para a direita
        else if (x < -0.01f)
            spriteRenderer.flipX = false;  // indo para a esquerda
    }

    private void FixedUpdate()
    {
        // Movimento suave
        Vector2 currentVel = rb.linearVelocity;
        float rate = (desiredVelocity.sqrMagnitude > 0.01f) ? acceleration : deceleration;

        rb.linearVelocity = Vector2.MoveTowards(currentVel, desiredVelocity, rate * Time.fixedDeltaTime);
    }
}
