using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Configurações da árvore")]
    public int vidaMaxima = 6;
    private int vidaAtual;

    [Header("Sprites da barra de vida")]
    public GameObject[] segmentosBarra; // Cada segmento é um GameObject (sprite)

    [Header("Madeira")]
    public int quantidadeMadeira = 1;

    [Header("Imagem de interação")]
    public GameObject imagemInteracao; // Arraste aqui a imagem (botão “E”)

    private bool jogadorProximo = false;

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarBarra();
        DesativarBarra(); // Começa invisível

        if (imagemInteracao != null)
            imagemInteracao.SetActive(false); // Esconde imagem no início
    }

    void Update()
    {
        if (jogadorProximo && Input.GetKeyDown(KeyCode.E))
        {
            Bater();
        }
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
            AtivarBarra();

            if (imagemInteracao != null)
                imagemInteracao.SetActive(true); // Mostra imagem
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorProximo = false;
            DesativarBarra();

            if (imagemInteracao != null)
                imagemInteracao.SetActive(false); // Esconde imagem
        }
    }

    private void AtivarBarra()
    {
        foreach (var segmento in segmentosBarra)
            if (segmento != null)
                segmento.SetActive(true);
    }

    private void DesativarBarra()
    {
        foreach (var segmento in segmentosBarra)
            if (segmento != null)
                segmento.SetActive(false);
    }
}
