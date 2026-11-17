using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaBehavior : MonoBehaviour
{
    AudioSource audioSource;

    private HudMoneda hudMoneda;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        hudMoneda = GameObject.FindGameObjectWithTag("Moneda")?.GetComponent<HudMoneda>();
    }

    void Update()
    {
         transform.Rotate (Vector3.up * 50 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
           //Aqui va el comportamiento para que sume puntaje (ke puntaje kbron!)
           hudMoneda.SumarMoneda();
           GameObject.Destroy(gameObject);
        }
    }
}
