using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonJugar : MonoBehaviour
{
    private Button boton;
    private string nombreEscena = "Nivel";

    private void Start()
    {
        boton = GetComponent<Button>();

        boton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(nombreEscena);

            Time.timeScale = 1f; //se activamos el juego
        });
    }
}
