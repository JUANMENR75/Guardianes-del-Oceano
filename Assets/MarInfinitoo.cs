using UnityEngine;

public class MarInfinitoo : MonoBehaviour
{
    [Header("Velocidad del oleaje")]
    public float velocidadX = 0.05f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (rend != null)
        {
            float offset = Time.time * velocidadX;
            rend.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}