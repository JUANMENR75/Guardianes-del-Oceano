using UnityEngine;

public class ControladorBarco : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadMovimiento = 5f;
    public float limiteSuperior = -0.8f; // Ajustado a tus valores reales de escena
    public float limiteInferior = -7f;

    [Header("Efecto de Flotar")]
    public float amplitudVertical = 0.3f;
    public float velocidadVertical = 2f;
    public float amplitudRotacion = 2f;
    public float velocidadRotacion = 1f;

    private float posicionBaseY;

    void Start()
    {
        posicionBaseY = transform.position.y;
    }

    void Update()
    {
        // Movimiento Vertical controlado por el jugador
        float inputY = Input.GetAxisRaw("Vertical");

        // Aplicamos el movimiento solo si hay entrada
        posicionBaseY += inputY * velocidadMovimiento * Time.deltaTime;

        // Mantenemos la posición dentro de los límites
        posicionBaseY = Mathf.Clamp(posicionBaseY, limiteInferior, limiteSuperior);

        // Efectos de Flotación (calculados sobre la base)
        float flotarY = Mathf.Sin(Time.time * velocidadVertical) * amplitudVertical;
        float rotacionZ = Mathf.Sin(Time.time * velocidadRotacion) * amplitudRotacion;

        // Aplicar transformación final
        transform.position = new Vector3(transform.position.x, posicionBaseY + flotarY, 0);
        transform.rotation = Quaternion.Euler(0, 0, rotacionZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Esta línea ahora sí encontrará a tu GestorPuntuacion
        GestorPuntuacion gestor = Object.FindAnyObjectByType<GestorPuntuacion>();

        if (collision.CompareTag("Moneda"))
        {
            if (gestor != null) gestor.SumarMoneda(); // Ahora esto funcionará
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Basura"))
        {
            if (gestor != null) gestor.SumarBasura(); // Ahora esto funcionará
            Destroy(collision.gameObject);
        }
    }
}