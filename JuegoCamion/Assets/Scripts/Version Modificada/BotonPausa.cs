using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BotonPausa : MonoBehaviour
{
    private GameObject panel; 
    private bool estaActivo = false;

    private Button boton;


    private void Start()
    {
        panel = transform.parent.Find("PanelPausa").gameObject; //obtenemos el panel y lo desactivamos al inicio
        panel.SetActive(false); 
        
        boton = GetComponent<Button>();

        boton.onClick.AddListener(() =>
        {
            Pausar();
        });
    }

    public void Pausar()
    {
        estaActivo = !estaActivo;
        panel.SetActive(estaActivo);

        Time.timeScale = estaActivo ? 0f : 1f; //pausamos y despausamos el juego
    }
}
