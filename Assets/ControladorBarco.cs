using UnityEngine;

public class ControladorBarco : MonoBehaviour
{
    [Header("Configuración del Teclado")]
    public float velocidadMovimiento = 300f;
    public float limiteSuperior = 200f;
    public float limiteInferior = -200f;

    [Header("Efecto de Flotar de las Olas")]
    public float amplitudVertical = 15f;
    public float velocidadVertical = 1.5f;
    public float amplitudRotacion = 5f;
    public float velocidadRotacion = 1.2f;

    private RectTransform rectTransform;
    private float posicionBaseY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posicionBaseY = rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        float inputY = Input.GetAxisRaw("Vertical");

        if (inputY != 0)
        {
            posicionBaseY += inputY * velocidadMovimiento * Time.deltaTime;
            posicionBaseY = Mathf.Clamp(posicionBaseY, limiteInferior, limiteSuperior);
        }

        float flotarY = Mathf.Sin(Time.time * velocidadVertical) * amplitudVertical;
        float rotacionZ = Mathf.Sin(Time.time * velocidadRotacion) * amplitudRotacion;

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, posicionBaseY + flotarY);
        rectTransform.rotation = Quaternion.Euler(0, 0, rotacionZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Esto imprimirá en la consola con qué objeto está chocando el bote
        Debug.Log("Chocaste con: " + collision.tag);

        GestorPuntuacion gestor = Object.FindAnyObjectByType<GestorPuntuacion>();

        if (collision.CompareTag("Basura"))
        {
            if (gestor != null) gestor.SumarBasura();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Moneda"))
        {
            if (gestor != null) gestor.SumarMoneda();
            Destroy(collision.gameObject);
        }
    }
} 