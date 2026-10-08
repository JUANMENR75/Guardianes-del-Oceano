using UnityEngine;

public class AjusteDecoracion : MonoBehaviour
{
    private Vector3 viewportPosition;

    void Start()
    {
        if (Camera.main != null)
        {
            viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        }
    }

    void Update()
    {
        if (Camera.main == null) return;

        Vector3 nuevaPosicionMundo = Camera.main.ViewportToWorldPoint(viewportPosition);
        transform.position = new Vector3(nuevaPosicionMundo.x, nuevaPosicionMundo.y, transform.position.z);
    }
}