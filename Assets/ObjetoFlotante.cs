using UnityEngine;

public class ObjetoFlotante : MonoBehaviour
{
    public float velocidad = 7.5f; // Aumentamos la velocidad para que se dispersen rápido

    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}