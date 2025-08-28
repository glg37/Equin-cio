using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Cr�ditos")]
    public GameObject creditsPanel; 

    // Bot�o Play
    public void PlayGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }

    // Bot�o Sair
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
