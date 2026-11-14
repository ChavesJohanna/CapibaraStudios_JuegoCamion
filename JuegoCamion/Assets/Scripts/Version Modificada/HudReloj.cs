using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudReloj : MonoBehaviour// se encuentra en el objeto Relog dento del PanelTiempo/Moneda del HUD
{
    private TextMeshProUGUI texto;

    private float tiempo = 180f; // 1:30 min

    private bool iniciarTiempo = false;

    private void Start()
    {
        texto = transform.Find("Texto").GetComponent<TextMeshProUGUI>();

        Invoke(nameof(IniciarTemporizador), 3f); //el nameof evita que ocurran errores si se escribe en string
    }

    private void Update()
    {
        if (iniciarTiempo)
        {
            tiempo -= Time.deltaTime;

            tiempo = Mathf.Max(0, tiempo); //evita que el tiempo tome valores negativos

            System.TimeSpan tiempoRestante = System.TimeSpan.FromSeconds(tiempo);

            texto.text = tiempoRestante.ToString(@"mm\:ss");
        }
    }

    private void IniciarTemporizador()
    {
        iniciarTiempo = true;
    }

    
}
