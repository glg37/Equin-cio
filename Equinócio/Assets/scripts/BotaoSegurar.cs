using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotaoSegurar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Bonfire bonfire; // Arraste o script Bonfire aqui

    private bool segurando = false;

    void Update()
    {
        if (segurando)
        {
            bonfire.AumentarBarraSegurar(Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        segurando = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        segurando = false;
    }
}
