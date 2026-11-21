using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesPanel : MonoBehaviour
{
    [Header("Pestañas (botones)")]
    [SerializeField] private Button botonControles;

    [Header("Pestañas (contenido: etiquetas + triángulo + controles)")]
    [SerializeField] private GameObject contenidoControles;

    [Header("Controles")]
    [SerializeField] private SelectorOpcion selectorModo;
    [SerializeField] private SelectorOpcion selectorPedales;

    [Header("Botones")]
    [SerializeField] private Button botonReestablecer;
    [SerializeField] private Button botonVolver;

    private GameObject panelOrigen;

    private void Awake()
    {
        botonControles.onClick.AddListener(() => MostrarPestana(0));

        selectorModo.Cambio += i => OpcionesJuego.Modo = (ModoManejo)i;
        selectorPedales.Cambio += i => OpcionesJuego.Pedales = (PosicionPedales)i;

        botonReestablecer.onClick.AddListener(Reestablecer);
        botonVolver.onClick.AddListener(Volver);
    }

    public void Abrir(GameObject origen)
    {
        panelOrigen = origen;
        if (panelOrigen != null) panelOrigen.SetActive(false);

        gameObject.SetActive(true);
        CargarValores();
        MostrarPestana(0);
    }

    private void Volver()
    {
        OpcionesJuego.Guardar();
        gameObject.SetActive(false);
        if (panelOrigen != null) panelOrigen.SetActive(true);
    }

    private void Reestablecer()
    {
        OpcionesJuego.Reestablecer();
        CargarValores();
    }

    private void OnDisable()
    {
        OpcionesJuego.Guardar();
    }

    private void MostrarPestana(int indice)
    {
        contenidoControles.SetActive(indice == 0);
    }

    private void CargarValores()
    {
        selectorModo.Fijar((int)OpcionesJuego.Modo);
        selectorPedales.Fijar((int)OpcionesJuego.Pedales);
    }
}
