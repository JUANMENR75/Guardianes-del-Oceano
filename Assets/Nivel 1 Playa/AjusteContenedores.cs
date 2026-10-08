using UnityEngine;

public class AjusteContenedores : MonoBehaviour
{
    [Header("Configuración de Margen")]
    public float margenDerecho = 1.0f; // Distancia desde el borde derecho de la pantalla

    void Update()
    {
        if (Camera.main == null) return;

        // Calcula la posición basada en los límites visibles de la cámara ortográfica 2D
        float distanciaZ = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 bordeDerechoMundo = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2f, distanciaZ));

        // Actualiza la posición X manteniendo su posición vertical (Y) y profundidad (Z) intactas
        transform.position = new Vector3(bordeDerechoMundo.x - margenDerecho, transform.position.y, transform.position.z);
    }
}