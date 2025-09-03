using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcDialogue : MonoBehaviour
{
    [Header("Configuração do Diálogo Inicial")]
    public string[] dialogueNpc;
    private int dialogueIndex;

    [Header("Configuração do Diálogo de Pós-Missão")]
    public string[] dialogueAfterMission;
    private int afterMissionIndex;
    private bool afterMissionDialogueActive;

    [Header("Referências de UI")]
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

        player = Object.FindFirstObjectByType<PlayerTopDownController>();
        if (player != null)
            playerAnimator = player.GetComponent<Animator>();

        StartDialogue();
    }

    void Update()
    {
        if (dialogueActive && Input.GetButtonDown("Fire1"))
        {
            ShowNextLine();
        }
        else if (afterMissionDialogueActive && Input.GetButtonDown("Fire1"))
        {
            ShowNextAfterMissionLine();
        }
    }

    void StartDialogue()
    {
        nameNpc.text = "Raseth";
        imageNpc.sprite = spriteNpc;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        dialogueActive = true;

        Time.timeScale = 0f;
        if (player != null) player.canMove = false;
        if (playerAnimator != null) playerAnimator.SetBool("isMoving", false);

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
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueActive = false;

        if (player != null) player.canMove = true;
        Time.timeScale = 1f;

        if (Inventory.instance != null)
            Inventory.instance.MostrarMissao();
    }

    //  Chamado quando completar a missão
    public void StartAfterMissionDialogue()
    {
        nameNpc.text = "Raseth";
        imageNpc.sprite = spriteNpc;
        afterMissionIndex = 0;
        dialoguePanel.SetActive(true);
        afterMissionDialogueActive = true;

        Time.timeScale = 0f;
        if (player != null) player.canMove = false;
        if (playerAnimator != null) playerAnimator.SetBool("isMoving", false);

        ShowNextAfterMissionLine();
    }

    void ShowNextAfterMissionLine()
    {
        if (afterMissionIndex < dialogueAfterMission.Length)
        {
            dialogueText.text = dialogueAfterMission[afterMissionIndex];
            afterMissionIndex++;
        }
        else
        {
            EndAfterMissionDialogue();
        }
    }

    void EndAfterMissionDialogue()
    {
        dialoguePanel.SetActive(false);
        afterMissionDialogueActive = false;

        if (player != null) player.canMove = true;
        Time.timeScale = 1f;
    }
}
