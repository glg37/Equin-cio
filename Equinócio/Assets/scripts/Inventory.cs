using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Header("Missões")]
    public bool missaoMadeiraConcluida = false;
    public bool missaoFogueirasConcluida = false;

    [Header("Madeira")]
    public int madeira = 0;
    public int madeiraMaxima = 15;
    public TextMeshProUGUI textoMadeira;

    [Header("Fogueiras")]
    public int fogueirasAcendidas = 0;
    public int fogueirasTotais = 3;
    public TextMeshProUGUI textoFogueiras;

    [Header("Folhas")]
    public int folhasColetadas = 0;
    public int folhasNecessarias = 10;
    public TextMeshProUGUI textoFolhas;

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
        if (textoMadeira != null) textoMadeira.gameObject.SetActive(false);
        if (textoFogueiras != null) textoFogueiras.gameObject.SetActive(false);
        if (textoFolhas != null) textoFolhas.gameObject.SetActive(false);
    }

    // ------------------- MADEIRA -------------------
    public void AdicionarMadeira(int quantidade)
    {
        madeira += quantidade;
        if (madeira > madeiraMaxima) madeira = madeiraMaxima;

        MostrarMissaoMadeira();

        if (madeira >= madeiraMaxima && !missaoMadeiraConcluida)
        {
            missaoMadeiraConcluida = true;
            Debug.Log("Missão de madeira concluída!");

            // Chama o diálogo pós-missão de madeira
            NpcDialogue npc = Object.FindFirstObjectByType<NpcDialogue>();
            if (npc != null)
                npc.StartAfterMissionDialogue();
        }
    }

    public bool RemoverMadeira(int quantidade)
    {
        if (madeira >= quantidade)
        {
            madeira -= quantidade;
            MostrarMissaoMadeira();
            return true;
        }
        return false;
    }

    public void MostrarMissaoMadeira()
    {
        if (textoMadeira != null)
        {
            textoMadeira.text = "Madeiras coletadas: " + madeira + "/" + madeiraMaxima;
            textoMadeira.gameObject.SetActive(true);
        }
        if (textoFogueiras != null) textoFogueiras.gameObject.SetActive(false);
        if (textoFolhas != null) textoFolhas.gameObject.SetActive(false);
    }

    // ------------------- FOGUEIRAS -------------------
    public void FogueiraAcendida()
    {
        fogueirasAcendidas++;
        MostrarMissaoFogueiras();

        // Se todas as fogueiras estiverem acesas
        if (fogueirasAcendidas >= fogueirasTotais && !missaoFogueirasConcluida)
        {
            missaoFogueirasConcluida = true;
            Debug.Log("Todas as fogueiras acesas! Agora a missão é coletar folhas.");

            // Ativa o objetivo das folhas sem abrir diálogo
            MostrarMissaoFolhas();
        }
    }

    public void MostrarMissaoFogueiras()
    {
        if (textoMadeira != null) textoMadeira.gameObject.SetActive(false);
        if (textoFogueiras != null)
        {
            textoFogueiras.text = "Acender fogueiras: " + fogueirasAcendidas + "/" + fogueirasTotais;
            textoFogueiras.gameObject.SetActive(true);
        }
        if (textoFolhas != null) textoFolhas.gameObject.SetActive(false);
    }

    // ------------------- FOLHAS -------------------
    public void AdicionarFolhas(int quantidade)
    {
        folhasColetadas += quantidade;
        if (folhasColetadas > folhasNecessarias) folhasColetadas = folhasNecessarias;

        MostrarMissaoFolhas();
    }

    public void MostrarMissaoFolhas()
    {
        if (textoMadeira != null) textoMadeira.gameObject.SetActive(false);
        if (textoFogueiras != null) textoFogueiras.gameObject.SetActive(false);
        if (textoFolhas != null)
        {
            textoFolhas.text = "Coletar folhas secas: " + folhasColetadas + "/" + folhasNecessarias;
            textoFolhas.gameObject.SetActive(true);
        }
    }
}
