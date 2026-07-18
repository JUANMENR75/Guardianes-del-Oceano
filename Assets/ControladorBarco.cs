using UnityEngine;

public class ControladorBarco : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Qué tan rápido responde el barco al presionar las teclas")]
    public float velocidadMovimiento = 5f;

    [Header("Límites de Pantalla")]
    public float limiteSuperior = -0.8f;
    public float limiteInferior = -7.8f;

    [Header("Configuración de Balanceo (Ajustable)")]
    [Tooltip("Qué tanto se inclina el barco (0 = recto)")]
    public float amplitudRotacion = 6.58f;
    [Tooltip("Qué tan rápido se inclina")]
    public float velocidadRotacion = 2f;

    private float posicionBaseY;

    // Referencia al script de puntuación
    private GestorPuntuacion gestorPuntuacion;

    // --- NUEVA VARIABLE ---
    [Header("Gestión de Fin de Juego")]
    public GestorGameOver gestorGameOver;

    void Start()
    {
        posicionBaseY = transform.position.y;
        gestorPuntuacion = FindAnyObjectByType<GestorPuntuacion>();
    }

    void Update()
    {
        // Movimiento controlado
        float inputY = Input.GetAxisRaw("Vertical");
        posicionBaseY += inputY * velocidadMovimiento * Time.deltaTime;
        posicionBaseY = Mathf.Clamp(posicionBaseY, limiteInferior, limiteSuperior);

        // Balanceo ajustable
        float rotacionZ = Mathf.Sin(Time.time * velocidadRotacion) * amplitudRotacion;

        // Aplicamos la posición y la rotación
        transform.position = new Vector3(transform.position.x, posicionBaseY, 0);
        transform.rotation = Quaternion.Euler(0, 0, rotacionZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gestorPuntuacion == null) return;

        if (collision.CompareTag("Moneda"))
        {
            gestorPuntuacion.SumarMoneda();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Basura"))
        {
            gestorPuntuacion.SumarBasura();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Bomba"))
        {
            // --- CÓDIGO CORREGIDO ---
            // Ya no se reinicia la escena, ahora activamos el Game Over
            if (gestorGameOver != null)
            {
                gestorGameOver.ActivarGameOver();
            }
        }
    }
}