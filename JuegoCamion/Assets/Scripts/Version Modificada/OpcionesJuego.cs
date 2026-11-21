using System;
using UnityEngine;
using UnityEngine.Audio;

public enum ModoManejo { Giroscopio = 0, Manual = 1 }
public enum PosicionPedales { Derecha = 0, Izquierda = 1 }

public static class OpcionesJuego
{
    //Valores predeterminados
    public const ModoManejo ModoDefecto = ModoManejo.Giroscopio;
    public const PosicionPedales PedalesDefecto = PosicionPedales.Derecha;

    private const string ClaveModo = "Opciones.ModoManejo";
    private const string ClavePedales = "Opciones.PosicionPedales";
    public static event Action Cambiado;

    public static ModoManejo Modo
    {
        get => (ModoManejo)Mathf.Clamp(PlayerPrefs.GetInt(ClaveModo, (int)ModoDefecto), 0, 1);
        set { PlayerPrefs.SetInt(ClaveModo, (int)value); Cambiado?.Invoke(); }
    }

    public static PosicionPedales Pedales
    {
        get => (PosicionPedales)Mathf.Clamp(PlayerPrefs.GetInt(ClavePedales, (int)PedalesDefecto), 0, 1);
        set { PlayerPrefs.SetInt(ClavePedales, (int)value); Cambiado?.Invoke(); }
    }

    public static void Guardar()
    {
        PlayerPrefs.Save();
    }

    public static void Reestablecer()
    {
        PlayerPrefs.SetInt(ClaveModo, (int)ModoDefecto);
        PlayerPrefs.SetInt(ClavePedales, (int)PedalesDefecto);
        Guardar();
        Cambiado?.Invoke();
    }
}
