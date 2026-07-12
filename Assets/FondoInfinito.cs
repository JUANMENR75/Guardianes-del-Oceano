using UnityEngine;
using UnityEngine.UI;

public class FondoInfinito : MonoBehaviour
{
    public float velocidad = 0.05f;
    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        if (rawImage != null)
        {
            rawImage.uvRect = new Rect(rawImage.uvRect.x + velocidad * Time.deltaTime, 0, rawImage.uvRect.width, rawImage.uvRect.height);
        }
    }
}