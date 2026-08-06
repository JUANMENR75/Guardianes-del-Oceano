using UnityEngine;

public class AnimacionBasura : MonoBehaviour
{
    // Estas variables serán independientes para cada objeto (Botella, Llanta, Bote)
    public float velocidad = 1.0f;
    public float angulo = 5.0f;
    public float altura = 3.0f;

    private Vector2 posInicial;
    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        posInicial = rt.anchoredPosition;
    }

    void Update()
    {
        // Rotación de lado a lado
        rt.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * velocidad) * angulo);

        // Movimiento de flotación (subir y bajar)
        rt.anchoredPosition = new Vector2(posInicial.x, posInicial.y + Mathf.Sin(Time.time * velocidad * 1.5f) * altura);
    }
}