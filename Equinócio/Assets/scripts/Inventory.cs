using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Header("Recursos")]
    public int madeira = 0;
    public int madeiraMaxima = 15;

    [Header("UI de Recursos")]
    public TextMeshProUGUI textoMadeira;

    private void Awake()
    {
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
        // UI só aparece depois do diálogo inicial
        if (textoMadeira != null)
            textoMadeira.gameObject.SetActive(false);
    }

    public void AdicionarMadeira(int quantidade)
    {
        madeira += quantidade;
        if (madeira > madeiraMaxima)
            madeira = madeiraMaxima;

        AtualizarUI();

        // Checa se completou a missão
        if (madeira >= madeiraMaxima)
        {
            MissaoConcluida();
        }
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
            textoMadeira.gameObject.SetActive(true);
        }
    }

    public void MostrarMissao()
    {
        AtualizarUI();
    }

    private void MissaoConcluida()
    {
        Debug.Log("Missão de coletar madeira concluída!");

        // chama o diálogo de pós-missão
        NpcDialogue npc = Object.FindFirstObjectByType<NpcDialogue>();
        if (npc != null)
        {
            npc.StartAfterMissionDialogue();
        }
    }
}
