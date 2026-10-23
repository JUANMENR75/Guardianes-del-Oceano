using UnityEngine;

public class ObjetoFlotante : MonoBehaviour
{
    [Header("Avance Lento Regulable")]
    [Tooltip("Velocidad muy suave con la que el mar arrastra el objeto hacia la izquierda")]
    public float velocidadAvance = 0.3f;

    [Header("Límite de Destrucción")]
    [Tooltip("Si el objeto cruza este límite a la izquierda, se destruye para liberar memoria")]
    public float limiteIzquierda = -5f;

    void Update()
    {
        // Avance sutil hacia la izquierda
        transform.Translate(Vector3.left * velocidadAvance * Time.deltaTime, Space.World);

        // Destruir si sale de la pantalla por la izquierda
        if (transform.position.x < limiteIzquierda)
        {
            Destroy(gameObject);
        }
    }
}