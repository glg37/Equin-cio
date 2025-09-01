using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Configurações da árvore")]
    public int vidaMaxima = 6;
    private int vidaAtual;

    [Header("Sprites da barra de vida")]
    public GameObject barraDeVida; // Objeto pai da barra
    public GameObject[] segmentosBarra; // Array de segmentos do maior para o menor

    [Header("Madeira")]
    public int quantidadeMadeira = 1;

    [Header("Imagem de interação")]
    public GameObject imagemInteracao; // Ex: botão “E”

    private bool jogadorProximo = false;

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarBarra();

        if (barraDeVida != null)
            barraDeVida.SetActive(false);

        if (imagemInteracao != null)
            imagemInteracao.SetActive(false);
    }

    void Update()
    {
        if (jogadorProximo && Input.GetKeyDown(KeyCode.E))
            Bater();
    }

    private void Bater()
    {
        vidaAtual--;
        AtualizarBarra();

        if (vidaAtual <= 0)
        {
            if (Inventory.instance != null)
                Inventory.instance.AdicionarMadeira(quantidadeMadeira);

            Destroy(gameObject);

            if (imagemInteracao != null)
                imagemInteracao.SetActive(false);
        }
    }

    private void AtualizarBarra()
    {
        if (barraDeVida != null)
            barraDeVida.SetActive(true);

        // Ativa/desativa segmentos conforme a vida
        for (int i = 0; i < segmentosBarra.Length; i++)
        {
            if (segmentosBarra[i] != null)
                segmentosBarra[i].SetActive(i < vidaAtual);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorProximo = true;

            if (barraDeVida != null)
                barraDeVida.SetActive(true);

            if (imagemInteracao != null)
                imagemInteracao.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorProximo = false;

            if (barraDeVida != null)
                barraDeVida.SetActive(false);

            if (imagemInteracao != null)
                imagemInteracao.SetActive(false);
        }
    }
}
