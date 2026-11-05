using UnityEngine;

public class AnimalesDecorativos : MonoBehaviour
{
    [Header("Límites de Pantalla")]
    public float limiteIzquierda = -9f;
    public float limiteDerecha = 9f;
    public float limiteArriba = 4f;
    public float limiteAbajo = -3.5f;

    [Header("Velocidad de Nado (Muy lento y suave)")]
    public float velocidadX = 0.2f;
    public float velocidadY = 0.1f;

    private float direccionX = 1f; // 1 = Avanza hacia la derecha (al frente)
    private float direccionY = 1f;

    void Start()
    {
        // TODOS empiezan obligatoriamente viendo hacia adelante (hacia la derecha) y avanzando hacia allá
        direccionX = 1f;
        direccionY = Random.value > 0.5f ? 1f : -1f;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = false; // Comienzan viendo al frente
        }
    }

    void Update()
    {
        // Mueve al animal
        transform.Translate(new Vector3(velocidadX * direccionX * Time.deltaTime, velocidadY * direccionY * Time.deltaTime, 0f));

        Vector3 pos = transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Si llega al límite derecho, cambia de dirección hacia la izquierda y VOLTEA el sprite para que su "frente" siga siendo hacia donde camina
        if (pos.x >= limiteDerecha)
        {
            pos.x = limiteDerecha;
            direccionX = -1f;
            if (sr != null) sr.flipX = true; // Se voltea para ir de frente hacia la izquierda
        }
        // Si llega al límite izquierdo, cambia de dirección hacia la derecha y DESVOLTEA el sprite para ir de frente
        else if (pos.x <= limiteIzquierda)
        {
            pos.x = limiteIzquierda;
            direccionX = 1f;
            if (sr != null) sr.flipX = false; // Se vuelve a poner de frente hacia la derecha
        }

        // Límites verticales
        if (pos.y >= limiteArriba)
        {
            pos.y = limiteArriba;
            direccionY = -1f;
        }
        else if (pos.y <= limiteAbajo)
        {
            pos.y = limiteAbajo;
            direccionY = 1f;
        }

        transform.position = pos;
    }
}