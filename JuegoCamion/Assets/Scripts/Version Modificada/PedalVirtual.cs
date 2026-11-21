using UnityEngine;
using UnityEngine.EventSystems;

public class PedalVirtual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public enum Tipo { Acelerar, Frenar }

    [SerializeField] private Tipo tipo;
    [SerializeField] private float escalaPresionado = 0.92f;

    public void OnPointerDown(PointerEventData eventData) { Establecer(true); }
    public void OnPointerUp(PointerEventData eventData) { Establecer(false); }
    public void OnPointerExit(PointerEventData eventData) { Establecer(false); }

    private void OnDisable() { Establecer(false); }

    private void Establecer(bool activo)
    {
        if (tipo == Tipo.Acelerar) ControlesEntrada.Acelerando = activo;
        else ControlesEntrada.Frenando = activo;

        transform.localScale = Vector3.one * (activo ? escalaPresionado : 1f);
    }
}
