using UnityEngine;

[RequireComponent(typeof(MovementControl))]
[RequireComponent(typeof(Rigidbody))]
public class SonidoCamion : MonoBehaviour
{
    [Header("Motor dinámico")]
    [SerializeField] private float velocidadMaximaAudio = 15f; // Velocidad a la que el motor llega al pitch máximo
    [SerializeField] private float pitchMinimo = 0.85f;         // Tono en ralentí (detenido)
    [SerializeField] private float pitchMaximo = 1.55f;         // Tono acelerando al máximo
    [SerializeField] private float volumenMinimo = 0.35f;
    [SerializeField] private float volumenMaximo = 0.75f;
    [SerializeField] private float suavizado = 5f;

    [Header("Pitido de reversa")]
    [SerializeField] private AudioClip sonidoReversa;         // Un clip corto de un solo "pip"
    [SerializeField] private float intervaloReversa = 0.65f;    // Cada cuántos segundos suena el "pip"
    [Range(0f, 1f)]
    [SerializeField] private float volumenReversa = 0.7f;

    private MovementControl movementControl;
    private Rigidbody rb;
    private AudioSource motorSource;
    private AudioSource sfxSource;

    private bool motorIniciado;
    private float temporizadorReversa;

    private void Start()
    {
        movementControl = GetComponent<MovementControl>();
        rb = GetComponent<Rigidbody>();

        motorSource = movementControl.sonidoMotor;
        sfxSource = movementControl.fxSource != null ? movementControl.fxSource : motorSource;

        if (motorSource != null)
        {
            motorSource.loop = true;
            motorSource.pitch = pitchMinimo;
            motorSource.volume = volumenMinimo;
            motorSource.Play();
            motorIniciado = true;
        }
    }

    private void Update()
    {
        // Si el juego está en pausa (Time.timeScale == 0), pausamos el motor y no sonamos reversa
        if (Time.timeScale == 0f)
        {
            if (motorSource != null && motorSource.isPlaying)
                motorSource.Pause();
            return;
        }

        if (motorSource != null && motorIniciado && !motorSource.isPlaying)
        {
            motorSource.UnPause();
        }

        ActualizarMotor();
        ActualizarPitidoReversa();
    }

    private void ActualizarMotor()
    {
        if (motorSource == null || rb == null) return;

        Vector3 velPlana = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float factorVelocidad = Mathf.Clamp01(velPlana.magnitude / velocidadMaximaAudio);

        // Si está pisando acelerador o reversa aunque recién arranque, sube levemente el esfuerzo
        bool pisandoPedal = ControlesEntrada.Acelerando ^ ControlesEntrada.Frenando;
        float factorObjetivo = Mathf.Max(factorVelocidad, pisandoPedal ? 0.2f : 0f);

        float pitchObjetivo = Mathf.Lerp(pitchMinimo, pitchMaximo, factorObjetivo);
        float volumenObjetivo = Mathf.Lerp(volumenMinimo, volumenMaximo, factorObjetivo);

        motorSource.pitch = Mathf.Lerp(motorSource.pitch, pitchObjetivo, suavizado * Time.deltaTime);
        motorSource.volume = Mathf.Lerp(motorSource.volume, volumenObjetivo, suavizado * Time.deltaTime);
    }

    private void ActualizarPitidoReversa()
    {
        if (sonidoReversa == null || sfxSource == null || rb == null) return;

        float velAdelante = Vector3.Dot(rb.velocity, transform.forward);

        // Coincide con la lógica de reversa de MovementControl.CalcularAceleracionManual
        bool enReversa = (ControlesEntrada.Frenando && !ControlesEntrada.Acelerando && velAdelante <= movementControl.umbralParada)
                         || velAdelante < -0.5f;

        if (!enReversa)
        {
            // En 0 para que al volver a entrar en reversa el primer "pip" suene al instante
            temporizadorReversa = 0f;
            return;
        }

        temporizadorReversa -= Time.deltaTime;
        if (temporizadorReversa <= 0f)
        {
            sfxSource.PlayOneShot(sonidoReversa, volumenReversa);
            temporizadorReversa = intervaloReversa;
        }
    }
}
