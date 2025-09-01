using UnityEngine;
using UnityEngine.UI;
using TMPro; // precisa importar para usar TextMeshPro
using System.Collections;

public class DayNightCycleUI : MonoBehaviour
{
    [Header("Referências")]
    public Image nightPanel;        // Arraste o painel aqui no inspector
    public GameObject defeatPanel;  // Painel de derrota
    public TMP_Text dayCounterText; // Texto TMP para mostrar dias restantes

    [Header("Configuração do ciclo")]
    public float dayDuration = 10f;    // Duração inicial do dia
    public float nightDuration = 5f;   // Duração inicial da noite
    public float fadeSpeed = 1f;       // Velocidade da transição
    public int totalDays = 5;          // Quantos dias o jogador tem

    [Header("Dificuldade progressiva")]
    public float dayDecreasePerCycle = 1f;   // Quanto o dia encurta a cada ciclo
    public float nightIncreasePerCycle = 1f; // Quanto a noite aumenta a cada ciclo

    private int currentDay;

    private void Start()
    {
        currentDay = totalDays;

        if (defeatPanel != null)
            defeatPanel.SetActive(false);

        UpdateDayCounterUI();

        StartCoroutine(DayNightCycle());
    }

    private IEnumerator DayNightCycle()
    {
        while (currentDay > 0)
        {
            // Espera pelo tempo de dia
            yield return new WaitForSeconds(dayDuration);

            // Escurece (entardecer)
            yield return StartCoroutine(FadePanel(0, 220));

            // Tempo de noite
            yield return new WaitForSeconds(nightDuration);

            // Clareia (amanhecer)
            yield return StartCoroutine(FadePanel(220, 0));

            // Passa 1 dia
            currentDay--;
            Debug.Log("Dias restantes: " + currentDay);
            UpdateDayCounterUI();

            // Dificuldade aumenta: dia menor, noite maior
            dayDuration = Mathf.Max(1f, dayDuration - dayDecreasePerCycle);
            nightDuration += nightIncreasePerCycle;
        }

        // Quando acabar os dias  derrota
        if (defeatPanel != null)
            defeatPanel.SetActive(true);
    }

    private IEnumerator FadePanel(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = nightPanel.color;

        while (elapsed < fadeSpeed)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeSpeed);
            nightPanel.color = new Color(color.r, color.g, color.b, alpha / 255f);
            yield return null;
        }

        nightPanel.color = new Color(color.r, color.g, color.b, endAlpha / 255f);
    }

    private void UpdateDayCounterUI()
    {
        if (dayCounterText != null)
            dayCounterText.text = "Dias restantes: " + currentDay;
    }
}
