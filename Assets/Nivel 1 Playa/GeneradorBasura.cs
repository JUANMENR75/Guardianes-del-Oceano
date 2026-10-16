using UnityEngine;

public class GeneradorBasura : MonoBehaviour
{
    [Header("Lista de Prefabs")]
    public GameObject[] prefabsBasura;

    [Header("Área de la Arena")]
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -3f;
    public float maxY = 1f;

    [Header("Configuración de Aparición")]
    public float tiempoInicio = 1f;
    public float intervaloSpawn = 2.5f;

    void Start()
    {
        InvokeRepeating("IntentarGenerarBasura", tiempoInicio, intervaloSpawn);
    }

    void IntentarGenerarBasura()
    {
        // Si el tiempo se acabó, ya no genera más basura
        ControladorTiempo tiempoControl = FindAnyObjectByType<ControladorTiempo>();
        if (tiempoControl != null && tiempoControl.TiempoAgotado()) return;

        if (prefabsBasura == null || prefabsBasura.Length == 0) return;

        Vector2 posicionLibre = Vector2.zero;
        bool posicionEncontrada = false;
        int intentos = 15; // Intentos para buscar un espacio vacío sin amontonar

        // Búsqueda de área libre (escaneo de espacio blanco)
        for (int i = 0; i < intentos; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 pruebaPos = new Vector2(randomX, randomY);

            // Comprueba si hay colisiones o basura cercana en esa posición
            Collider2D hit = Physics2D.OverlapCircle(pruebaPos, 0.6f);
            if (hit == null) // Si está libre
            {
                posicionLibre = pruebaPos;
                posicionEncontrada = true;
                break;
            }
        }

        // Si encontró un lugar limpio, genera la basura ahí
        if (posicionEncontrada)
        {
            int randomIndex = Random.Range(0, prefabsBasura.Length);
            GameObject basuraSeleccionada = prefabsBasura[randomIndex];

            GameObject nuevaBasura = Instantiate(basuraSeleccionada, posicionLibre, Quaternion.identity);
            nuevaBasura.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            SpriteRenderer sr = nuevaBasura.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = 5;
        }
    }
}