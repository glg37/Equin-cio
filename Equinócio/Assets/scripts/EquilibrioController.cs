using UnityEngine;
using UnityEngine.UI;

public class EquilibrioController : MonoBehaviour
{
    [Header("Configura��o da Barra")]
    public Slider barraEquilibrio; // arraste o Slider do Canvas aqui
    public float valorEquilibrio = 0.5f; // 0 = sombra total, 1 = luz total
    public float taxaMudanca = 0.1f; // velocidade que aumenta/diminui

    [Header("Refer�ncia do Jogador")]
    public Transform player;

    void Start()
    {
        // Ajusta o slider no meio
        barraEquilibrio.minValue = 0f;
        barraEquilibrio.maxValue = 1f;
        barraEquilibrio.value = valorEquilibrio;
    }

    void Update()
    {
        // Checar se o player est� em �rea clara ou escura
        if (EstaNaLuz())
        {
            valorEquilibrio += taxaMudanca * Time.deltaTime;
        }
        else if (EstaNaSombra())
        {
            valorEquilibrio -= taxaMudanca * Time.deltaTime;
        }

        // Limitar entre 0 e 1
        valorEquilibrio = Mathf.Clamp01(valorEquilibrio);

        // Atualizar a barra
        barraEquilibrio.value = valorEquilibrio;
    }

    bool EstaNaLuz()
    {
        // Exemplo: qualquer posi��o X > 0 � luz
        return player.position.x > 0;
    }

    bool EstaNaSombra()
    {
        // Exemplo: qualquer posi��o X <= 0 � sombra
        return player.position.x <= 0;
    }
}
