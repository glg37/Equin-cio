using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcDialogue : MonoBehaviour
{
    [Header("Configura��o do Di�logo")]
    public string[] dialogueNpc;       // falas do personagem
    private int dialogueIndex;

    [Header("Refer�ncias de UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;

    private PlayerTopDownController player;
    private Animator playerAnimator;
    private bool dialogueActive;

    void Start()
    {
        dialoguePanel.SetActive(false);

        // acha o player e o animator
        player = Object.FindFirstObjectByType<PlayerTopDownController>();
        if (player != null)
            playerAnimator = player.GetComponent<Animator>();

        // inicia o di�logo automaticamente
        StartDialogue();
    }

    void Update()
    {
        if (dialogueActive && Input.GetButtonDown("Fire1")) // bot�o esquerdo do mouse
        {
            ShowNextLine();
        }
    }

    void StartDialogue()
    {
        nameNpc.text = "Raseth";
        imageNpc.sprite = spriteNpc;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        dialogueActive = true;

        // pausa o jogo inteiro
        Time.timeScale = 0f;

        // trava o player
        if (player != null)
            player.canMove = false;

        // pausa anima��o de movimento
        if (playerAnimator != null)
            playerAnimator.SetBool("isMoving", false);

        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (dialogueIndex < dialogueNpc.Length)
        {
            dialogueText.text = dialogueNpc[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            EndDialogue(); // chama a fun��o final ao terminar o di�logo
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueActive = false;

        // libera o player
        if (player != null)
            player.canMove = true;

        // retoma o jogo
        Time.timeScale = 1f;

        // MOSTRA A MISS�O AP�S O DI�LOGO
        if (Inventory.instance != null)
        {
            Inventory.instance.MostrarMissao();
        }
    }
}
