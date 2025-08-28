using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMenuController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float moveSpeed = 2f;
    public Vector2 limiteEsquerdaDireita = new Vector2(-3f, 3f);

    private Rigidbody2D rb;
    private Animator anim;
    private int direcao = 1; 

    private float escalaOriginalX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

      
        escalaOriginalX = transform.localScale.x;
    }

    void Update()
    {
        
        rb.linearVelocity = new Vector2(direcao * moveSpeed, rb.linearVelocity.y);

       
        anim.SetBool("isWalking", true);

      
        if (transform.position.x > limiteEsquerdaDireita.y && direcao == 1)
            Virar();
        else if (transform.position.x < limiteEsquerdaDireita.x && direcao == -1)
            Virar();
    }

    void Virar()
    {
        direcao *= -1;

        Vector3 scale = transform.localScale;

        
        scale.x = direcao == -1 ? Mathf.Abs(escalaOriginalX) : -Mathf.Abs(escalaOriginalX);

        transform.localScale = scale;
    }
}
