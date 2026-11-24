using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tyreMeshes = new Transform[4];
    public float maxTorque = 100.0f;
    private Rigidbody m_rigidbody;
    public Transform centerOfMass;
    public float acclSensitivity = 5.0f;
    public float speed;
    public float fuerzaFreno = 20000f;
    public float desaceleracionFreno = 10f; //0 = solo frenan las ruedas, 0 > frena cada vez más en seco
    public float potenciaReversa = 0.6f;
    public float velocidadMaximaReversa = 8f;
    public float umbralParada = 0.5f;


    public int contAmarillo = 0;
    public int contVerde = 0;
    public int contAzul = 0;
    public int contRojo = 0;

    HudManager hud;

    public AudioSource fxSource;
    public AudioSource sonidoMotor;
    public AudioClip sonidoDejar;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.centerOfMass = centerOfMass.localPosition;

        hud = FindObjectOfType<HudManager>();
    }

    void Update()
    {
        UpdateMeshesPositions();
    }

    void FixedUpdate()
    {
        // GIROSCOPIO: dirige inclinando el dispositivo. MANUAL: dirige con el volante en pantalla.
        float steer = OpcionesJuego.Modo == ModoManejo.Giroscopio ? Input.acceleration.x : ControlesEntrada.Direccion;
        float fixedAngle = steer * 45f;
        wheelColliders[0].steerAngle = fixedAngle;
        wheelColliders[1].steerAngle = fixedAngle;

        // En ambos modos se acelera, frena y da reversa con los pedales del HUD.
        float acceleration = CalcularAceleracionManual(out bool frenando);

        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = acceleration * maxTorque;
            wheelColliders[i].brakeTorque = frenando ? fuerzaFreno : 0f;
        }

        if (frenando) FrenadoDeEmergencia();
    }

    float CalcularAceleracionManual(out bool frenando)
    {
        float velAdelante = Vector3.Dot(m_rigidbody.velocity, transform.forward);
        bool acelerador = ControlesEntrada.Acelerando;
        bool pedalFreno = ControlesEntrada.Frenando;
        frenando = false;

        if (acelerador && pedalFreno)
        {
            frenando = true;
            return 0f;
        }

        if (pedalFreno)
        {
            if (velAdelante > umbralParada)
            {
                frenando = true; 
                return 0f;
            }
            if (velAdelante > -velocidadMaximaReversa)
                return -speed * potenciaReversa;
            return 0f;
        }

        if (acelerador)
        {
            if (velAdelante < -umbralParada)
            {
                frenando = true;
                return 0f;
            }
            return speed;
        }

        return 0f;
    }

    //Ayuda que el camión frene más rápido
    void FrenadoDeEmergencia()
    {
        Vector3 v = m_rigidbody.velocity;
        Vector3 plano = new Vector3(v.x, 0f, v.z);
        plano = Vector3.MoveTowards(plano, Vector3.zero, desaceleracionFreno * Time.fixedDeltaTime);
        m_rigidbody.velocity = new Vector3(plano.x, v.y, plano.z);
    }

    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tyreMeshes[i].position = pos;
            tyreMeshes[i].rotation = quat;
        }
    }

    public void PlaySoundLeaving()
    {
        sonidoMotor.PlayOneShot(sonidoDejar);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Depositos

        if (other.gameObject.CompareTag("DepositoAmarillo") && contAmarillo <= 5)
        {
            if (contAmarillo != 0)
            {
                hud.DepositarResiduos("Amarillo", contAmarillo);
                PlaySoundLeaving();
            }
            contAmarillo = 0;
            Debug.Log("Amarillo: " + contAmarillo);
        }

        if (other.gameObject.CompareTag("DepositoVerde") && contVerde <= 5)
        {
            if (contVerde != 0)
            {
                hud.DepositarResiduos("Verde", contVerde);
                PlaySoundLeaving();
            }
            contVerde = 0;
            Debug.Log("Verde: " + contVerde);
        }

        if (other.gameObject.CompareTag("DepositoAzul") && contAzul <= 5)
        {
            if (contAzul != 0)
            {
                hud.DepositarResiduos("Azul", contAzul);
                PlaySoundLeaving();
            }
            contAzul = 0;
            Debug.Log("Azul: " + contAzul);
        }

        if (other.gameObject.CompareTag("DepositoRojo") && contRojo <= 5)
        {
            if (contRojo != 0)
            {
                hud.DepositarResiduos("Rojo", contRojo);
                PlaySoundLeaving();
            }
            contRojo = 0;
            Debug.Log("Rojo: " + contRojo);
        }
    }
}
