using UnityEngine;

public class ObjetoFlotante : MonoBehaviour
{
    public float velocidadBase = 5f;

    void Update()
    {
        // Se mueve a la izquierda multiplicando por el factor de dificultad global
        float velocidadActual = velocidadBase * ControladorDificultad.multiplicadorVelocidad;
        transform.Translate(Vector3.left * velocidadActual * Time.deltaTime);
    }
}