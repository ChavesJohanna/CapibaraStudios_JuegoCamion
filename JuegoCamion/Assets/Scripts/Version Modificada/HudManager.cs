using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    private enum TipoResiduo {Amarillo = 0, Verde = 1, Azul = 2, Rojo = 3}

    private int puntosPorRecoger = 10;
    private int puntosPorDepositar = 10;
    private float segundosPorDepositar = 10f;

    [Header("Textos de basura (0=Plastico, 1=Vidrio, 2=Papel, 3=Peligroso)")]
    [SerializeField] private TextMeshProUGUI[] textosBasura = new TextMeshProUGUI[4];


    private HudPuntaje hudPuntaje;
    private HudReloj hudReloj;
    private bool partidaTerminada;

    private void Awake()
    {
        hudPuntaje = GetComponentInChildren<HudPuntaje>(true);
        hudReloj = GetComponentInChildren<HudReloj>(true);

        for (int i = 0; i < textosBasura.Length; i++)
        {
            if (textosBasura[i] != null)
                textosBasura[i].text = "0";
            else
                Debug.LogError($"HudManager: falta asignar el texto de basura en la posici�n {i}.", this);
        }

    }

    private void Update()
    {
        if (!partidaTerminada && hudReloj != null && hudReloj.TiempoAgotado)
            TerminarPartida();
    }

    public void RecogerResiduo(string tipo, int cantidadActual)
    {
        ActualizarTextoBasura(tipo, cantidadActual);
        hudPuntaje?.SumarPuntaje(puntosPorRecoger);
    }

    public void DepositarResiduos(string tipo, int cantidad)
    {
        if (cantidad > 0)
        {
            hudPuntaje?.SumarPuntaje(puntosPorDepositar * cantidad);
            hudReloj?.SumarTiempo(segundosPorDepositar * cantidad);
        }
        ActualizarTextoBasura(tipo, 0);
    }

    private void ActualizarTextoBasura(string tipo, int cantidad)
    {
        if (Enum.TryParse(tipo, out TipoResiduo indice) && Enum.IsDefined(typeof(TipoResiduo), indice))
        {
            int i = (int)indice;
            if (i >= 0 && i < textosBasura.Length && textosBasura[i] != null)
            {
                textosBasura[i].text = cantidad.ToString();
                return;
            }
        }

        Debug.LogWarning("HudManager: tipo desconocido o texto sin asignar: " + tipo, this);
    }

    private void TerminarPartida()
    {
        partidaTerminada = true;
        PlayerPrefs.SetFloat("Puntaje", hudPuntaje != null ? hudPuntaje.Puntaje : 0f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Derrota");
    }
}