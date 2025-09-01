using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Header("Recursos")]
    public int madeira = 0;
    public int madeiraMaxima = 15; // Máximo de madeira que pode coletar

    [Header("UI de Recursos")]
    public TextMeshProUGUI textoMadeira; // Arraste o TMPUGUI do Canvas aqui

    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AtualizarUI();
    }

    public void AdicionarMadeira(int quantidade)
    {
        madeira += quantidade;

        // Garante que não passe do máximo
        if (madeira > madeiraMaxima)
            madeira = madeiraMaxima;

        AtualizarUI();
    }

    public bool RemoverMadeira(int quantidade)
    {
        if (madeira >= quantidade)
        {
            madeira -= quantidade;
            AtualizarUI();
            return true;
        }
        return false;
    }

    private void AtualizarUI()
    {
        if (textoMadeira != null)
        {
            textoMadeira.text = "Madeiras coletadas: " + madeira + "/" + madeiraMaxima;
        }
    }
}
