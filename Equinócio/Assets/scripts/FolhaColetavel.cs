using UnityEngine;

public class FolhaColetavel : MonoBehaviour
{
    public int quantidade = 1; // Quantidade de folhas
    public GameObject imagemInteracao; // O bot�o "E"

    private bool jogadorProximo = false;

    private void Start()
    {
        if (imagemInteracao != null)
            imagemInteracao.SetActive(false); // Come�a escondido
    }

    private void Update()
    {
        // S� permite coletar se o jogador estiver pr�ximo E a miss�o das fogueiras estiver conclu�da
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
        // Mostra bot�o E apenas se miss�o das fogueiras conclu�da
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