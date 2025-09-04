using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject painelVitoria; // Arraste o painel do Canvas aqui
    public Inventory inventario; // Arraste seu Inventory aqui

    private void Update()
    {
        if (inventario != null && inventario.folhasColetadas >= inventario.folhasNecessarias)
        {
            MostrarVitoria();
        }
    }

    private void MostrarVitoria()
    {
        if (painelVitoria != null && !painelVitoria.activeSelf)
        {
            painelVitoria.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo
        }
    }

    public void VoltarParaMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu"); 
    }
}
