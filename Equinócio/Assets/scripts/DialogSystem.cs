using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI do diálogo")]
    public GameObject dialogPanel;        // Painel do diálogo
    public Image characterImage;          // Imagem do personagem
    public TextMeshProUGUI dialogText;    // Texto do diálogo

    [Header("Configuração do diálogo")]
    public Sprite[] characterSprites;     // Sprites dos personagens
    [TextArea(3, 10)]
    public string[] messages;             // Mensagens do diálogo
    public int[] characterIndex;          // Índice do personagem para cada mensagem

    private int currentMessage = 0;
    private bool dialogActive = false;

    void Start()
    {
        dialogPanel.SetActive(false);
    }

    void Update()
    {
        if (dialogActive && Input.GetMouseButtonDown(0))
        {
            NextMessage();
        }
    }

    public void StartDialog()
    {
        if (messages.Length == 0) return;

        dialogActive = true;
        currentMessage = 0;
        dialogPanel.SetActive(true);
        ShowMessage();
    }

    private void ShowMessage()
    {
        if (currentMessage >= messages.Length)
        {
            EndDialog();
            return;
        }

        // Atualiza a imagem do personagem
        if (characterIndex[currentMessage] >= 0 && characterIndex[currentMessage] < characterSprites.Length)
            characterImage.sprite = characterSprites[characterIndex[currentMessage]];

        // Atualiza o texto
        dialogText.text = messages[currentMessage];
    }

    private void NextMessage()
    {
        currentMessage++;
        ShowMessage();
    }

    private void EndDialog()
    {
        dialogActive = false;
        dialogPanel.SetActive(false);
    }
}
