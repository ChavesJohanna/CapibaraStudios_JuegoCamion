using UnityEngine;
using UnityEngine.UI;

public class BotonSalir : MonoBehaviour
{
    private Button boton;

    private void Start()
    {
        boton = GetComponent<Button>();

        boton.onClick.AddListener(() =>
        {
            Application.Quit(); //cierra el ejecutable del juego

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //desactiva el modo play del editor
            #endif
        });
    }
}
