using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Créditos")]
    public GameObject creditsPanel; 

    // Botão Play
    public void PlayGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }

    // Botão Sair
    public void ExitGame()
    {
        Debug.Log("Saiu do jogo!");
        Application.Quit(); 
    }

    public void OpenCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }
}
