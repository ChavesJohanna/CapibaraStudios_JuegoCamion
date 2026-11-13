using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonReanudar : MonoBehaviour
{
    private BotonPausa botonPausa;

    private Button boton;


    private void Start()
    {
        GameObject panel = transform.parent.gameObject;

        botonPausa = panel.transform.parent.GetComponentInChildren<BotonPausa>(); //llegamos al HUD y luego agarramos al hijo que tiene el script

        boton = GetComponent<Button>();

        boton.onClick.AddListener(() =>
        {
            botonPausa.Pausar();
        });
    }
}
