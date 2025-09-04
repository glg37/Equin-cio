using UnityEngine;
using UnityEngine.SceneManagement; // Para voltar ao menu principal

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Arraste o Canvas do menu de pausa aqui

    private bool isPaused = false;

    void Update()
    {
        // Ativar/desativar o pause ao apertar ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);   
        Time.timeScale = 1f;             
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);     
        Time.timeScale = 0f;             
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Botão Menu clicado!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    
}
