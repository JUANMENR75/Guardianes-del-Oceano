using UnityEngine;

public class MoverNube : MonoBehaviour
{
    public float velocidad = 20f;
    public float limiteDerecho = 1000f;
    public float posicionInicialX = -1000f;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.right * velocidad * Time.deltaTime;

        if (rectTransform.anchoredPosition.x > limiteDerecho)
        {
            rectTransform.anchoredPosition =
                new Vector2(posicionInicialX, rectTransform.anchoredPosition.y);
        }
    }
}