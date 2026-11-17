using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudMoneda : MonoBehaviour//esta en el objeto con el mismo nombre del hud
{
    private TextMeshProUGUI texto;

    private int cantMonedas = 0;

    private void Start()
    {
        texto = transform.Find("Texto")?.GetComponent<TextMeshProUGUI>();

        texto.text = cantMonedas.ToString(); //setiamos el texto a 0
    }

    public void SumarMoneda()
    {
        cantMonedas++;
        texto.text = cantMonedas.ToString();
    }
}
