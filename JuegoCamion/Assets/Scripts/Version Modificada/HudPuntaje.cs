using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudPuntaje : MonoBehaviour
{
    private TextMeshProUGUI texto;

    private int puntajeActual = 0;

    private void Start()
    {
        texto = transform.Find("Texto")?.GetComponent<TextMeshProUGUI>();

        texto.text = puntajeActual.ToString(); //setiamos el texto a 0
    }

    public void SumarPuntaje()
    {
        puntajeActual++;
        texto.text = puntajeActual.ToString();
    }
}
