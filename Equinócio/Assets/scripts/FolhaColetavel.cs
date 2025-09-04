using UnityEngine;

public class FolhaColetavel : MonoBehaviour
{
    public int quantidade = 1; // Quantidade de folhas
    public GameObject imagemInteracao; // O botão "E"

    private bool jogadorProximo = false;

    private void Start()
    {
        if (imagemInteracao != null)
            imagemInteracao.SetActive(false); // Começa escondido
    }

    private void Update()
    {
        // Só permite coletar se o jogador estiver próximo E a missão das fogueiras estiver concluída
        if (jogadorProximo && Inventory.instance != null
            && Inventory.instance.missaoFogueirasConcluida
            && Input.GetKeyDown(KeyCode.E))
        {
            Coletar();
        }
    }

    private void Coletar()
    {
        if (Inventory.instance != null)
            Inventory.instance.AdicionarFolhas(quantidade);

        if (imagemInteracao != null)
            imagemInteracao.SetActive(false);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mostra botão E apenas se missão das fogueiras concluída
        if (collision.CompareTag("Player") && Inventory.instance != null
            && Inventory.instance.missaoFogueirasConcluida)
        {
            jogadorProximo = true;
            if (imagemInteracao != null)
                imagemInteracao.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorProximo = false;
            if (imagemInteracao != null)
                imagemInteracao.SetActive(false);
        }
    }
}