using UnityEngine;

public class MoverNube : MonoBehaviour
{
    [Tooltip("Qué tan rápido oscila la nube (0.5 es un buen punto de partida)")]
    public float frecuencia = 0.5f;

    [Tooltip("Qué tan lejos se mueve a cada lado desde su origen")]
    public float distancia = 50f;

    private Vector2 posicionInicial;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posicionInicial = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // La frecuencia controla la velocidad del ciclo (el vaivén)
        float movimiento = Mathf.Sin(Time.time * frecuencia) * distancia;

        rectTransform.anchoredPosition = new Vector2(posicionInicial.x + movimiento, rectTransform.anchoredPosition.y);
    }
}