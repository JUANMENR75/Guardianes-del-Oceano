using UnityEngine; // Esto es obligatorio

public class ObjetoFlotante : MonoBehaviour // "ObjetoFlotante" debe coincidir con el nombre del archivo
{
    public float velocidad = 0.2f;

    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}