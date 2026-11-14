using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonIrAlMenu : MonoBehaviour
{
    private Button boton;
    private string nombreEscena = "Menu";

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
