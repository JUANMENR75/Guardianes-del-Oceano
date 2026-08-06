using UnityEngine;

public class BalanceoDecorativo : MonoBehaviour
{
    public float velocidad = 1f; // Ritmo del balanceo
    public float anguloInclinacion = 0f; // Qué tanto se inclina (grados)

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Usamos Sin para crear una oscilación suave
        float rotacionZ = Mathf.Sin(Time.time * velocidad) * anguloInclinacion;

        // Aplicamos la rotación en el eje Z (el que inclina el bote)
        rectTransform.localRotation = Quaternion.Euler(0, 0, rotacionZ);
    }
}