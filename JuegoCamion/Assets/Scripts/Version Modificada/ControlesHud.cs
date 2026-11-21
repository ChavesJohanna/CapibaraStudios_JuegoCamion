using UnityEngine;

public class ControlesHud : MonoBehaviour
{
    [SerializeField] private RectTransform pedalAcelerador;
    [SerializeField] private RectTransform pedalFreno;
    [SerializeField] private RectTransform volante;

    private struct Anclaje
    {
        public Vector2 min, max, pivote, posicion;
    }

    private Anclaje baseAcelerador, baseFreno, baseVolante;

    private void Awake()
    {
        baseAcelerador = Guardar(pedalAcelerador);
        baseFreno = Guardar(pedalFreno);
        baseVolante = Guardar(volante);
    }

    private void OnEnable()
    {
        OpcionesJuego.Cambiado += Aplicar;
        Aplicar();
    }

    private void OnDisable()
    {
        OpcionesJuego.Cambiado -= Aplicar;
    }

    private void Aplicar()
    {
        bool espejar = OpcionesJuego.Pedales == PosicionPedales.Izquierda;

        Colocar(pedalAcelerador, baseAcelerador, espejar);
        Colocar(pedalFreno, baseFreno, espejar);
        Colocar(volante, baseVolante, espejar);

        bool manual = OpcionesJuego.Modo == ModoManejo.Manual;
        volante.gameObject.SetActive(manual);
        pedalAcelerador.gameObject.SetActive(manual);
        pedalFreno.gameObject.SetActive(manual);
    }

    private static Anclaje Guardar(RectTransform r)
    {
        return new Anclaje { min = r.anchorMin, max = r.anchorMax, pivote = r.pivot, posicion = r.anchoredPosition };
    }

    private static void Colocar(RectTransform r, Anclaje a, bool espejar)
    {
        if (!espejar)
        {
            r.anchorMin = a.min; r.anchorMax = a.max; r.pivot = a.pivote; r.anchoredPosition = a.posicion;
            return;
        }

        r.anchorMin = new Vector2(1f - a.max.x, a.min.y);
        r.anchorMax = new Vector2(1f - a.min.x, a.max.y);
        r.pivot = new Vector2(1f - a.pivote.x, a.pivote.y);
        r.anchoredPosition = new Vector2(-a.posicion.x, a.posicion.y);
    }
}
