using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bonfire : MonoBehaviour
{
    [Header("UI")]
    public GameObject spriteE;
    public GameObject painelMinigame;
    public Image barraProgresso;

    [Header("Fogueira")]
    public GameObject prefabFogueiraAcesa; // Prefab da fogueira já acesa

    [Header("Configuração")]
    public int madeiraNecessaria = 2;  // Madeira consumida por fogueira
    public float aumentoPorClique = 0.2f;
    public float tempoParaZerar = 3f;

    private bool playerPerto = false;
    private bool minigameAtivo = false;
    private bool fogueiraCriada = false;
    private float progresso = 0f;

    private PlayerTopDownController player;

    void Start()
    {
        // Cria botão E dinamicamente se não estiver atribuído
        if (spriteE == null)
        {
            GameObject canvasObj = GameObject.Find("Canvas");
            if (canvasObj == null)
            {
                canvasObj = new GameObject("Canvas");
                Canvas canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }

            spriteE = new GameObject("BotaoE");
            spriteE.transform.SetParent(canvasObj.transform);

            Image img = spriteE.AddComponent<Image>();
            // Aqui você pode adicionar o sprite que quiser
            // img.sprite = seuSprite;

            RectTransform rt = spriteE.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            rt.anchorMin = new Vector2(0.5f, 0.2f);
            rt.anchorMax = new Vector2(0.5f, 0.2f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;

            spriteE.SetActive(false);
        }

        if (painelMinigame != null) painelMinigame.SetActive(false);
        if (barraProgresso != null) barraProgresso.fillAmount = 0f;

        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerTopDownController>();
    }

    void Update()
    {
        if (!playerPerto) return;

        // Só permite iniciar minigame se todas as madeiras foram coletadas
        bool todasMadeirasColetadas = Inventory.instance != null && Inventory.instance.madeira >= Inventory.instance.madeiraMaxima;

        if (spriteE != null)
            spriteE.SetActive(todasMadeirasColetadas && !minigameAtivo && !fogueiraCriada);

        // Inicia minigame de clique
        if (todasMadeirasColetadas && Input.GetKeyDown(KeyCode.E))
        {
            if (!minigameAtivo && !fogueiraCriada)
                IniciarMinigameClique();
        }

        // Minigame clique
        if (minigameAtivo)
        {
            if (player != null) player.canMove = false;

            progresso -= Time.deltaTime / tempoParaZerar;
            if (progresso < 0f) progresso = 0f;
            if (barraProgresso != null) barraProgresso.fillAmount = progresso;

            if (Input.GetMouseButtonDown(0))
            {
                progresso += aumentoPorClique;
                if (progresso > 1f) progresso = 1f;
                if (barraProgresso != null) barraProgresso.fillAmount = progresso;

                if (progresso >= 1f)
                    InstanciarFogueiraAcesa();
            }
        }
    }

    void IniciarMinigameClique()
    {
        if (painelMinigame != null) painelMinigame.SetActive(true);
        minigameAtivo = true;
        progresso = 0f;
        if (barraProgresso != null) barraProgresso.fillAmount = 0f;
        if (spriteE != null) spriteE.SetActive(false);
    }

    void InstanciarFogueiraAcesa()
    {
        minigameAtivo = false;
        fogueiraCriada = true;

        if (painelMinigame != null) painelMinigame.SetActive(false);

        // Remove madeira necessária para a fogueira
        if (Inventory.instance != null)
            Inventory.instance.RemoverMadeira(madeiraNecessaria);

        // Instancia o prefab da fogueira já acesa
        if (prefabFogueiraAcesa != null)
        {
            Instantiate(prefabFogueiraAcesa, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab da fogueira acesa não foi atribuído!");
        }

        if (player != null)
            player.canMove = true;

        Debug.Log("Fogueira instanciada e acesa!");
    }

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
            if (spriteE != null) spriteE.SetActive(false);
            if (painelMinigame != null) painelMinigame.SetActive(false);
            if (barraProgresso != null) barraProgresso.fillAmount = 0f;
            minigameAtivo = false;
        }
    }
}
