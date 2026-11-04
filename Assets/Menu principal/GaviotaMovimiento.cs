using UnityEngine;

public class MovimientoGaviota : MonoBehaviour
{
    [Header("Configuración de Vuelo")]
    [Tooltip("Velocidad de movimiento en píxeles por segundo.")]
    public float velocidad = 150f; 

    [Tooltip("Margen extra en píxeles para que la gaviota salga por completo de la pantalla antes de teletransportarse.")]
    public float margenSalida = 100f; 

    private RectTransform rectTransform;
    private Canvas canvasPadre;

    void Start()
    {
        // Obtenemos el RectTransform necesario para mover elementos de UI
        rectTransform = GetComponent<RectTransform>();

        // Buscamos el Canvas principal en los objetos superiores
        canvasPadre = GetComponentInParent<Canvas>();

        if (canvasPadre == null)
        {
            Debug.LogError("¡Ojo! El objeto de la gaviota debe estar dentro de un Canvas para que este script funcione correctamente.");
        }
    }

    void Update()
    {
        if (canvasPadre == null) return;

        // 1. Mover la gaviota hacia la izquierda continuamente
        rectTransform.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;

        // 2. Obtener el ancho actual del Canvas (se adapta a cualquier resolución de pantalla)
        float anchoCanvas = canvasPadre.GetComponent<RectTransform>().rect.width;

        // 3. Calcular los límites izquierdo y derecho basándonos en el centro del Canvas
        float limiteIzquierdo = -(anchoCanvas / 2f) - margenSalida;
        float limiteDerecho = (anchoCanvas / 2f) + margenSalida;

        // 4. Si la gaviota cruza el límite izquierdo, la teletransportamos al derecho
        if (rectTransform.anchoredPosition.x < limiteIzquierdo)
        {
            rectTransform.anchoredPosition = new Vector2(limiteDerecho, rectTransform.anchoredPosition.y);
        }
    }
}