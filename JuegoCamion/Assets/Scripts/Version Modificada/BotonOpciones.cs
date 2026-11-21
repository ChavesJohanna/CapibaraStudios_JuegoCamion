using UnityEngine;
using UnityEngine.UI;

public class BotonOpciones : MonoBehaviour
{
    private Button boton;
    private OpcionesPanel panelOpciones;
    private GameObject panelOrigen;

    private void Start()
    {
        panelOrigen = transform.parent.gameObject;
        panelOpciones = panelOrigen.transform.parent.Find("PanelOpciones")?.GetComponent<OpcionesPanel>();

        if (panelOpciones == null)
        {
            Debug.LogError("BotonOpciones: no hay un PanelOpciones como hermano de " + panelOrigen.name, this);
            return;
        }

        boton = GetComponent<Button>();
        boton.onClick.AddListener(() =>
        {
            panelOpciones.Abrir(panelOrigen);
        });
    }
}
