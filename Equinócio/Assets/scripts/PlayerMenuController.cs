using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMenuController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float moveSpeed = 2f;
    public Vector2 limiteEsquerdaDireita = new Vector2(-3f, 3f);

    private Rigidbody2D rb;
    private Animator anim;
    private int direcao = 1; // 1 = direita, -1 = esquerda

    private float escalaOriginalX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Guarda a escala inicial (já olhando para ESQUERDA)
        escalaOriginalX = transform.localScale.x;
    }

    void Update()
    {
        // Movimento automático
        rb.linearVelocity = new Vector2(direcao * moveSpeed, rb.linearVelocity.y);

        // Animação de andar sempre ligada
        anim.SetBool("isWalking", true);

        // Chegou nos limites? Vira
        if (transform.position.x > limiteEsquerdaDireita.y && direcao == 1)
            Virar();
        else if (transform.position.x < limiteEsquerdaDireita.x && direcao == -1)
            Virar();
    }

    void Virar()
    {
        direcao *= -1;

        Vector3 scale = transform.localScale;

        // Como o sprite original olha para ESQUERDA:
        // Esquerda = escala original
        // Direita  = espelhado
        scale.x = direcao == -1 ? Mathf.Abs(escalaOriginalX) : -Mathf.Abs(escalaOriginalX);

        transform.localScale = scale;
    }
}
