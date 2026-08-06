using UnityEngine;

public class ObjetoFlotante : MonoBehaviour
{
    public float velocidadBase = 5f;

    void Update()
    {
        // Movimiento puro a la izquierda sin afectar a Y
        float velocidadActual = velocidadBase * ControladorDificultad.multiplicadorVelocidad;
        transform.Translate(Vector3.left * velocidadActual * Time.deltaTime, Space.World);

        // Destrucción al salir de pantalla
        if (transform.position.x < -12f) Destroy(gameObject);
    }
}