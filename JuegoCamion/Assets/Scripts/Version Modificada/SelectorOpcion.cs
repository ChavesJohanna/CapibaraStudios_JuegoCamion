using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectorOpcion : MonoBehaviour
{
    [SerializeField] private Button botonAnterior;
    [SerializeField] private Button botonSiguiente;

    [Header("Valor")]
    [SerializeField] private Image imagenValor;
    [SerializeField] private Sprite[] sprites;

    public int Indice { get; private set; }
    public event Action<int> Cambio;

    private int Cantidad => Mathf.Max(sprites != null ? sprites.Length : 0);

    private void Awake()
    {
        botonAnterior.onClick.AddListener(() => Mover(-1));
        botonSiguiente.onClick.AddListener(() => Mover(1));
    }
    public void Fijar(int indice)
    {
        if (Cantidad == 0) return;
        Indice = Mathf.Clamp(indice, 0, Cantidad - 1);
        Mostrar();
    }

    private void Mover(int paso)
    {
        if (Cantidad == 0) return;
        Indice = (Indice + paso + Cantidad) % Cantidad;
        Mostrar();
        Cambio?.Invoke(Indice);
    }

    private void Mostrar()
    {
        if (imagenValor != null && sprites != null && Indice < sprites.Length) imagenValor.sprite = sprites[Indice];
    }
}
