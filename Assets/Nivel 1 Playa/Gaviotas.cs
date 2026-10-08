using UnityEngine;

public class Gaviotas : MonoBehaviour
{
    [Header("Configuración de Vuelo")]
    [Tooltip("Velocidad de movimiento en píxeles por segundo (ej. 300 o 400).")]
    public float velocidad = 300f;

    [Tooltip("Margen extra en píxeles para que la gaviota salga por completo de la pantalla antes de reaparecer.")]
    public float margenSalida = 100f;

    private RectTransform rectTransform;
    private Canvas canvasPadre;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasPadre = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (canvasPadre == null) return;

        // Si el juego terminó (por ejemplo, si el panel de Game Over está activo), la gaviota se detiene
        GameObject panelGameOver = GameObject.Find("PanelGameOver");
        if (panelGameOver != null && panelGameOver.activeSelf)
        {
            return; // Detiene el movimiento de la gaviota
        }

        // 1. Mover la gaviota hacia la izquierda continuamente usando píxeles de UI
        rectTransform.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;

        // 2. Obtener el ancho actual del Canvas adaptado a cualquier pantalla
        float anchoCanvas = canvasPadre.GetComponent<RectTransform>().rect.width;

        // 3. Calcular límites izquierdo y derecho basados en el tamaño del Canvas
        float limiteIzquierdo = -(anchoCanvas / 2f) - margenSalida;
        float limiteDerecho = (anchoCanvas / 2f) + margenSalida;

        // 4. Si la gaviota cruza completamente el límite izquierdo, se regenera en el lado derecho
        if (rectTransform.anchoredPosition.x < limiteIzquierdo)
        {
            rectTransform.anchoredPosition = new Vector2(limiteDerecho, rectTransform.anchoredPosition.y);
        }
    }
}