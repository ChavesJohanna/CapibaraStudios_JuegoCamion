using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstruosSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monstruoPrefab;
    [SerializeField] private Transform[] puntos;

    private void Start()
    {
        if (monstruoPrefab == null)
        {
            Debug.LogError("MonstruosSpawner: falta asignar el prefab del monstruo.", this);
            return;
        }

        int creados = 0;
        foreach (Transform punto in puntos)
        {
            if (punto == null) continue;

            GameObject go = Instantiate(monstruoPrefab, punto.position, punto.rotation);
            MonstruoBehaviour monstruo = go.GetComponent<MonstruoBehaviour>();
            creados++;
        }

        Debug.Log("MonstruosSpawner: " + creados + " monstruos creados.");
    }
}