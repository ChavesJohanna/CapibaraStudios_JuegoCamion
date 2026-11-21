using UnityEngine;
using UnityEngine.EventSystems;
public class JoystickVirtual : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform imagenVolante;
    [SerializeField] private float anguloMaximo = 90f;

    private RectTransform zona;

    private void Awake() { zona = (RectTransform)transform; }

    public void OnPointerDown(PointerEventData e) { Actualizar(e); }
    public void OnDrag(PointerEventData e) { Actualizar(e); }
    public void OnPointerUp(PointerEventData e) { Soltar(); }
    private void OnDisable() { Soltar(); }

    private void Actualizar(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(zona, e.position, e.pressEventCamera, out Vector2 local);
        float radio = zona.rect.width * 0.5f;
        ControlesEntrada.Direccion = Mathf.Clamp((local.x - zona.rect.center.x) / radio, -1f, 1f);

        if (imagenVolante != null)
            imagenVolante.localRotation = Quaternion.Euler(0f, 0f, -ControlesEntrada.Direccion * anguloMaximo);
    }

    private void Soltar()
    {
        ControlesEntrada.Direccion = 0f;
        if (imagenVolante != null) imagenVolante.localRotation = Quaternion.identity;
    }
}