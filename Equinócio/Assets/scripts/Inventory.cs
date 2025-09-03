using UnityEngine;
using TMPro;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Header("Recursos")]
    public int madeira = 0;
    public int madeiraMaxima = 15; // M�ximo de madeira que pode coletar

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

    // N�O atualiza a UI no Start
    private void Start()
    {
        // A UI s� vai aparecer depois do di�logo
    }

    public void AdicionarMadeira(int quantidade)
    {
        madeira += quantidade;

        // Garante que n�o passe do m�ximo
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
            textoMadeira.gameObject.SetActive(true); // garante que fique vis�vel
        }
    }

    // Fun��o p�blica para mostrar a UI da miss�o
    public void MostrarMissao()
    {
        AtualizarUI();
    }
}
