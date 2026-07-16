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

    void Start()
    {
        posicionBaseY = transform.position.y;
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
        GestorPuntuacion gestor = Object.FindAnyObjectByType<GestorPuntuacion>();

        if (collision.CompareTag("Moneda"))
        {
            if (gestor != null) gestor.SumarMoneda();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Basura"))
        {
            if (gestor != null) gestor.SumarBasura();
            Destroy(collision.gameObject);
        }
    }
}