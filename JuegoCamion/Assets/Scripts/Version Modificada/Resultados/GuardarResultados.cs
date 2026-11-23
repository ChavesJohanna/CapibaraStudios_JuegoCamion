using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GuardarResultados
{
    private static int puntaje;
    private static int moneda;


    public static void GuardarPuntaje(int p)
    {
        puntaje = p;
    }
    public static void GuardarMoneda(int m)
    {
        moneda = m;
    }

    public static string RecuperarPuntajeFinal()
    {
        return puntaje.ToString();
    }
    public static string RecuperarMonedaFinal()
    {
        return moneda.ToString();
    }

    public static void ReiniciarResultados()
    {
        puntaje = 0;
        moneda = 0;
    }
}
