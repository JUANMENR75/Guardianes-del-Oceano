using UnityEngine;

public class ControladorDificultad : MonoBehaviour
{
    // Esta variable es estática para que cualquier script pueda ver la velocidad actual
    public static float multiplicadorVelocidad = 1.0f;

    [Tooltip("Qué tan rápido aumenta la velocidad. 0.05 es un ritmo suave.")]
    public float tasaAumento = 0.02f;

    public float velocidadMaxima = 3.0f; // Límite para que no sea imposible

    void Update()
    {
        // Aumenta el multiplicador con el tiempo
        if (multiplicadorVelocidad < velocidadMaxima)
        {
            multiplicadorVelocidad += tasaAumento * Time.deltaTime;
        }
    }
}