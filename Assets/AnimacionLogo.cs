using UnityEngine;

public class AnimacionLogo : MonoBehaviour
{
    public float velocidad = 1f;
    public float amplitud = 0.05f;

    private Vector3 escalaInicial;

    void Start()
    {
        escalaInicial = transform.localScale;
    }

    void Update()
    {
        float escala = 1 + Mathf.Sin(Time.time * velocidad) * amplitud;
        transform.localScale = escalaInicial * escala;
    }
}