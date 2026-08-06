using UnityEngine;

public class MovimientoNubes : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Velocidad de movimiento de la nube en píxeles por segundo.")]
    public float velocidad = 30f; 

    [Tooltip("Margen extra en píxeles para que la nube salga por completo de la pantalla antes de reaparecer.")]
    public float margenSalida = 250f; 

    private RectTransform rectTransform;
    private Canvas canvasPadre;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasPadre = GetComponentInParent<Canvas>();

        if (canvasPadre == null)
        {
            Debug.LogError("¡Ojo! El objeto de la nube debe estar dentro de un Canvas.");
        }
    }

    void Update()
    {
        if (canvasPadre == null) return;

        rectTransform.anchoredPosition += Vector2.right * velocidad * Time.deltaTime;

        float anchoCanvas = canvasPadre.GetComponent<RectTransform>().rect.width;

        float limiteDerecho = (anchoCanvas / 2f) + margenSalida;
        float limiteIzquierdo = -(anchoCanvas / 2f) - margenSalida;

        if (rectTransform.anchoredPosition.x > limiteDerecho)
        {
            rectTransform.anchoredPosition = new Vector2(limiteIzquierdo, rectTransform.anchoredPosition.y);
        }
    }
}