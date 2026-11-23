using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonedaFinal : MonoBehaviour
{
    private TextMeshProUGUI texto;

    private void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();

        texto.text = GuardarResultados.RecuperarMonedaFinal();
    }
}
