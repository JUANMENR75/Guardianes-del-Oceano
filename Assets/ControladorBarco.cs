using UnityEngine;

public class ControladorBarco : MonoBehaviour
{
    [Header("Configuración del Teclado")]
    public float velocidadMovimiento = 300f;
    public float limiteSuperior = 200f;
    public float limiteInferior = -200f;

    [Header("Efecto de Flotar de las Olas")]
    public float amplitudVertical = 15f; // Ajustado para Canvas (UI)
    public float velocidadVertical = 1.5f;
    public float amplitudRotacion = 5f;
    public float velocidadRotacion = 1.2f;

    private RectTransform rectTransform;
    private float posicionBaseY;
    private int puntaje = 0;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // Guardamos la posición Y en la que empieza el barco
        posicionBaseY = rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        // 1. CONTROL CON EL TECLADO (Flechas o W/S)
        float inputY = Input.GetAxisRaw("Vertical");

        if (inputY != 0)
        {
            // Modificamos la posición base hacia donde el jugador quiere ir
            posicionBaseY += inputY * velocidadMovimiento * Time.deltaTime;
            // Evitamos que se salga de los límites del mar
            posicionBaseY = Mathf.Clamp(posicionBaseY, limiteInferior, limiteSuperior);
        }

        // 2. EFECTO AUTOMÁTICO DE FLOTAR (Seno matemático)
        float flotarY = Mathf.Sin(Time.time * velocidadVertical) * amplitudVertical;
        float rotacionZ = Mathf.Sin(Time.time * velocidadRotacion) * amplitudRotacion;

        // 3. APLICAR TODO AL RECTRANSFORM
        // Sumamos la posición que quiere el jugador + el pequeño sube y baja automático del agua
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, posicionBaseY + flotarY);
        rectTransform.rotation = Quaternion.Euler(0, 0, rotacionZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Basura"))
        {
            puntaje += 10;
            Debug.Log("¡Basura limpia! Puntaje: " + puntaje);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Moneda"))
        {
            puntaje += 50;
            Debug.Log("¡Moneda recogida! Puntaje: " + puntaje);
            Destroy(collision.gameObject);
        }
    }
}