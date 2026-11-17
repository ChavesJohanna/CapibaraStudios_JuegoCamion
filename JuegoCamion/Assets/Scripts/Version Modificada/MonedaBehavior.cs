using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaBehavior : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         transform.Rotate (Vector3.up * 50 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
           //Aqui va el comportamiento para que sume puntaje
           GameObject.Destroy(gameObject);
        }
    }
}
