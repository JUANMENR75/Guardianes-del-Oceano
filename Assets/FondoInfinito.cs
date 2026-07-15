using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    public float velocidad = 0.1f;
    private Material mat;

    void Start()
    {
        // Obtenemos el material del Renderer
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Desplazamos el offset de la textura principal (_MainTex es el nombre estándar del shader)
        float offset = Time.time * velocidad;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}