using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{

    MovementControl player;
    HudManager hud;
    bool recogida = false; //evita contar dos veces si el camión toca la basura con más de un collider
    AudioSource sonidoRecoger;

    private void Start()
    {
        player = FindObjectOfType<MovementControl>();
        hud = FindObjectOfType<HudManager>();

        GameObject objSonido = GameObject.Find("SonidoRecoger");
        if (objSonido != null)
            sonidoRecoger = objSonido.GetComponent<AudioSource>();
        else
            Debug.LogWarning("TrashBehaviour: no se encontró el objeto 'SonidoRecoger' en la escena.", this);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (recogida || player == null || hud == null || !other.gameObject.CompareTag("Player")) return;

        if (this.gameObject.CompareTag("Amarillo") && player.contAmarillo < 5)
        {
            recogida = true;
            player.contAmarillo++;
            hud.RecogerResiduo("Amarillo", player.contAmarillo);
            Destroy(this.gameObject);
            Debug.Log("Amarillo: " + player.contAmarillo);
            sonidoRecoger.PlayOneShot(sonidoRecoger.clip);
        }

        else if (this.gameObject.CompareTag("Verde") && player.contVerde < 5)
        {
            recogida = true;
            player.contVerde++;
            hud.RecogerResiduo("Verde", player.contVerde);
            Destroy(this.gameObject);
            Debug.Log("Verde: " + player.contVerde);
            sonidoRecoger.PlayOneShot(sonidoRecoger.clip);
        }

        else if (this.gameObject.CompareTag("Azul") && player.contAzul < 5)
        {
            recogida = true;
            player.contAzul++;
            hud.RecogerResiduo("Azul", player.contAzul);
            Destroy(this.gameObject);
            Debug.Log("Azul: " + player.contAzul);
            sonidoRecoger.PlayOneShot(sonidoRecoger.clip);
        }

        else if (this.gameObject.CompareTag("Rojo") && player.contRojo < 5)
        {
            recogida = true;
            player.contRojo++;
            hud.RecogerResiduo("Rojo", player.contRojo);
            Destroy(this.gameObject);
            Debug.Log("Rojo: " + player.contRojo);
            sonidoRecoger.PlayOneShot(sonidoRecoger.clip);
        }
    }
}