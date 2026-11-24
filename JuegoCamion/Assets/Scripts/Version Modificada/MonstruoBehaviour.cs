using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstruoBehaviour : MonoBehaviour
{
    [Header("Recorrido")]
    [SerializeField] private float distanciaCaminata = 4f;
    [SerializeField] private float velocidad = 1.5f;
    [SerializeField] private float velocidadGiro = 360f;

    [Header("Efecto de caminata")]
    [SerializeField] private Transform modelo;
    [SerializeField] private float alturaRebote = 0.08f;
    [SerializeField] private float frecuenciaRebote = 8f;

    [Header("Chocado")]
    [SerializeField] private int puntos = 30;
    [SerializeField] private float segundosAplastado = 0.4f;

    private Vector3 extremoA, extremoB;
    private bool haciaB = true;
    private bool eliminado;
    private Vector3 modeloPosBase;
    private HudManager hud;

    public void Configurar(float distancia, float vel)
    {
        distanciaCaminata = distancia;
        velocidad = vel;
    }

    private void Start()
    {
        Vector3 dir = transform.forward;
        dir.y = 0f;
        dir = dir.sqrMagnitude > 0.0001f ? dir.normalized : Vector3.forward;

        extremoA = transform.position - dir * distanciaCaminata * 0.5f;
        extremoB = transform.position + dir * distanciaCaminata * 0.5f;

        if (modelo == null) modelo = transform;
        modeloPosBase = modelo.localPosition;

        hud = FindObjectOfType<HudManager>();
        if (hud == null)
            Debug.LogError("MonstruoPatrulla: no se encontró HudManager en la escena.", this);
    }

    private void Update()
    {
        if (eliminado) return;

        Vector3 actual = transform.position;
        Vector3 objetivo = haciaB ? extremoB : extremoA;
        Vector3 destino = new Vector3(objetivo.x, actual.y, objetivo.z);

        Vector3 nueva = Vector3.MoveTowards(actual, destino, velocidad * Time.deltaTime);
        transform.position = nueva;

        Vector3 haciaDestino = destino - nueva;
        haciaDestino.y = 0f;
        if (haciaDestino.sqrMagnitude < 0.0001f)
            haciaB = !haciaB; // gira
        else
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(haciaDestino), velocidadGiro * Time.deltaTime);

        if (modelo != transform)
            modelo.localPosition = modeloPosBase + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * frecuenciaRebote)) * alturaRebote;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (eliminado || hud == null || !other.gameObject.CompareTag("Player")) return;

        eliminado = true;
        hud.SumarPuntos(puntos);

        GetComponent<Collider>().enabled = false;

        Vector3 escala = modelo.localScale;
        modelo.localScale = new Vector3(escala.x, escala.y * 0.15f, escala.z);

        float espera = segundosAplastado;
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && audio.clip != null)
        {
            audio.Play();
            espera = Mathf.Max(espera, audio.clip.length);
        }

        Destroy(gameObject, espera);
    }

    // Vista previa del recorrido al seleccionar el monstruo
    private void OnDrawGizmosSelected()
    {
        Vector3 a, b;
        if (Application.isPlaying)
        {
            a = extremoA;
            b = extremoB;
        }
        else
        {
            Vector3 dir = transform.forward;
            dir.y = 0f;
            dir.Normalize();
            a = transform.position - dir * distanciaCaminata * 0.5f;
            b = transform.position + dir * distanciaCaminata * 0.5f;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(a, b);
        Gizmos.DrawWireSphere(a, 0.3f);
        Gizmos.DrawWireSphere(b, 0.3f);
    }
}
