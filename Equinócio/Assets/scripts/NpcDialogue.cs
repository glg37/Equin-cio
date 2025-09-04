using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    [Header("Diálogo Inicial")]
    public string[] dialogueNpc;
    private int dialogueIndex;

    [Header("Pós-missão Madeira")]
    public string[] dialogueAfterMission;
    private int afterMissionIndex;
    private bool afterMissionDialogueActive;

    [Header("Pós-fogueiras")]
    public string[] dialogueAfterBonfires;
    private int afterBonfiresIndex;
    private bool afterBonfiresActive;

    [Header("UI")]
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
            ShowNextLine();
        else if (afterMissionDialogueActive && Input.GetButtonDown("Fire1"))
            ShowNextAfterMissionLine();
        else if (afterBonfiresActive && Input.GetButtonDown("Fire1"))
            ShowNextAfterBonfiresLine();
    }

    // ---------------- DIÁLOGO INICIAL ----------------
    void StartDialogue()
    {
        nameNpc.text = "Raseth";
        imageNpc.sprite = spriteNpc;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        dialogueActive = true;

        Time.timeScale = 0f;
        if (player != null) player.canMove = false;

        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (dialogueIndex < dialogueNpc.Length)
            dialogueText.text = dialogueNpc[dialogueIndex++];
        else
            EndDialogue();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueActive = false;

        if (player != null)
        {
            player.canMove = true;
            if (playerAnimator != null) playerAnimator.SetBool("isMoving", false);
        }

        Time.timeScale = 1f;

        if (Inventory.instance != null)
            Inventory.instance.MostrarMissaoMadeira();
    }

    // ------------- PÓS-MADEIRA ----------------
    public void StartAfterMissionDialogue()
    {
        nameNpc.text = "Raseth";
        imageNpc.sprite = spriteNpc;
        afterMissionIndex = 0;
        dialoguePanel.SetActive(true);
        afterMissionDialogueActive = true;

        Time.timeScale = 0f;
        if (player != null) player.canMove = false;

        ShowNextAfterMissionLine();
    }

    void ShowNextAfterMissionLine()
    {
        if (afterMissionIndex < dialogueAfterMission.Length)
            dialogueText.text = dialogueAfterMission[afterMissionIndex++];
        else
            EndAfterMissionDialogue();
    }

    void EndAfterMissionDialogue()
    {
        dialoguePanel.SetActive(false);
        afterMissionDialogueActive = false;

        if (player != null)
        {
            player.canMove = true;
            if (playerAnimator != null) playerAnimator.SetBool("isMoving", false);
        }

        Time.timeScale = 1f;

        if (Inventory.instance != null)
            Inventory.instance.MostrarMissaoFogueiras();
    }

    // ------------- PÓS-FOGUEIRAS ----------------
    public void StartDialogueAfterBonfires()
    {
        nameNpc.text = "Raseth";
        imageNpc.sprite = spriteNpc;
        afterBonfiresIndex = 0;
        dialoguePanel.SetActive(true);
        afterBonfiresActive = true;

        Time.timeScale = 0f;
        if (player != null) player.canMove = false;

        ShowNextAfterBonfiresLine();
    }

    void ShowNextAfterBonfiresLine()
    {
        if (afterBonfiresIndex < dialogueAfterBonfires.Length)
            dialogueText.text = dialogueAfterBonfires[afterBonfiresIndex++];
        else
            EndAfterBonfiresDialogue();
    }

    void EndAfterBonfiresDialogue()
    {
        dialoguePanel.SetActive(false);
        afterBonfiresActive = false;

        if (player != null)
        {
            player.canMove = true;
            if (playerAnimator != null) playerAnimator.SetBool("isMoving", false);
        }

        Time.timeScale = 1f;
    }
}
