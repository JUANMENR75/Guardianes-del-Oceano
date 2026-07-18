using UnityEngine;

public class MoverMar : MonoBehaviour
{
    public float velocidad = 100f;
    public float ancho = 950f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;

        if (rect.anchoredPosition.x < -ancho - 475f)
        {
            rect.anchoredPosition += new Vector2(ancho * 2, 0);
        }
    }
}
