using UnityEngine;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    [Header("UI")]
    public GameObject spriteE;              // Indica para apertar E
    public GameObject painelMinigame;       // Painel do minigame de clique
    public Image barraProgresso;            // Barra de clique
    public GameObject painelSegurar;        // Painel do minigame de segurar
    public Image barraSegurar;              // Barra de segurar

    [Header("Fogueira")]
    public GameObject fogueiraGameObject;   // Fogueira na cena (começa desativada)
    public Animator animFogueira;           // Animator para animação de fogo

    [Header("Configuração")]
    public int madeiraNecessaria = 2;
    public float aumentoPorClique = 0.2f;   // Quanto a barra aumenta por clique
    public float tempoParaZerar = 3f;       // Decaimento da barra de clique
    public float tempoParaSegurar = 2f;     // Tempo necessário para segurar a barra
    public float tempoParaDecair = 1.5f;    // Decaimento da barra de segurar se soltar

    [HideInInspector] public bool botaoSegurando = false;

    private bool playerPerto = false;
    private bool minigameAtivo = false;
    private bool segurandoAtivo = false;
    private bool fogueiraCriada = false;
    private float progresso = 0f;
    private float progressoSegurar = 0f;

    void Start()
    {
        spriteE.SetActive(false);
        painelMinigame.SetActive(false);
        painelSegurar.SetActive(false);
        barraProgresso.fillAmount = 0f;
        barraSegurar.fillAmount = 0f;

        if (fogueiraGameObject != null)
            fogueiraGameObject.SetActive(false); // Começa invisível
    }

    void Update()
    {
        if (!playerPerto) return;

        bool temMadeira = Inventory.instance != null && Inventory.instance.madeira >= madeiraNecessaria;
        spriteE.SetActive(temMadeira && !minigameAtivo && !segurandoAtivo);

        // Iniciar minigame de clique
        if (temMadeira && Input.GetKeyDown(KeyCode.E))
        {
            if (!minigameAtivo && !segurandoAtivo && !fogueiraCriada)
                IniciarMinigameClique();
        }

        // Atualização do minigame de clique
        if (minigameAtivo)
        {
            progresso -= Time.deltaTime / tempoParaZerar;
            if (progresso < 0f) progresso = 0f;
            barraProgresso.fillAmount = progresso;

            if (Input.GetMouseButtonDown(0))
            {
                progresso += aumentoPorClique;
                if (progresso > 1f) progresso = 1f;
                barraProgresso.fillAmount = progresso;

                if (progresso >= 1f)
                    MostrarFogueiraApagada(); // Quando termina, fogueira aparece
            }
        }

        // Minigame de segurar: decaimento se soltar
        if (segurandoAtivo && !botaoSegurando)
        {
            progressoSegurar -= Time.deltaTime / tempoParaDecair;
            if (progressoSegurar < 0f) progressoSegurar = 0f;
            barraSegurar.fillAmount = progressoSegurar;
        }
    }

    // ----------------- MINIGAME DE CLIQUE -----------------
    void IniciarMinigameClique()
    {
        painelMinigame.SetActive(true);
        minigameAtivo = true;
        progresso = 0f;
        barraProgresso.fillAmount = 0f;
        spriteE.SetActive(false);
    }

    void MostrarFogueiraApagada()
    {
        if (Inventory.instance != null)
            Inventory.instance.RemoverMadeira(madeiraNecessaria);

        painelMinigame.SetActive(false);
        minigameAtivo = false;
        fogueiraCriada = true;

        // Ativa a fogueira apagada (sprite sem fogo)
        if (fogueiraGameObject != null)
            fogueiraGameObject.SetActive(true);

        // Ativa painel de segurar
        painelSegurar.SetActive(true);
        progressoSegurar = 0f;
        barraSegurar.fillAmount = 0f;
        spriteE.SetActive(true);
        segurandoAtivo = true;
    }

    // ----------------- MINIGAME DE SEGURAR -----------------
    public void AumentarBarraSegurar(float delta)
    {
        if (!segurandoAtivo) return;

        progressoSegurar += delta / tempoParaSegurar;
        if (progressoSegurar > 1f) progressoSegurar = 1f;
        barraSegurar.fillAmount = progressoSegurar;

        if (progressoSegurar >= 1f)
            AcenderFogueira();
    }

    void AcenderFogueira()
    {
        segurandoAtivo = false;
        painelSegurar.SetActive(false);
        spriteE.SetActive(false);

        // Dispara a animação de fogo
        if (animFogueira != null)
            animFogueira.SetTrigger("Acender");

        Debug.Log("Fogueira acesa!");
    }

    // ----------------- TRIGGER DO PLAYER -----------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerPerto = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;
            spriteE.SetActive(false);
            painelMinigame.SetActive(false);
            painelSegurar.SetActive(false);
            progresso = 0f;
            progressoSegurar = 0f;
            barraProgresso.fillAmount = 0f;
            barraSegurar.fillAmount = 0f;
            minigameAtivo = false;
            segurandoAtivo = false;
            botaoSegurando = false;
        }
    }
}
