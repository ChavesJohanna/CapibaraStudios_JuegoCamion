using UnityEngine;
public class MonedasSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monedaPrefab;
    [SerializeField] private Transform[] puntos;

    private void Start()
    {
        if (monedaPrefab == null)
        {
            Debug.LogError("MonedasSpawner: falta asignar el prefab de la moneda.", this);
            return;
        }

        int creadas = 0;
        foreach (Transform punto in puntos)
        {
            if (punto == null) continue;

            Instantiate(monedaPrefab, punto.position, monedaPrefab.transform.rotation);
            creadas++;
        }

        Debug.Log("MonedasSpawner: " + creadas + " monedas creadas.");
    }
}
