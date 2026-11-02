using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MovimientoSubmarino : MonoBehaviour
{
    public float velocidad = 6f;

    [Header("Límites de la Pantalla")]
    public float limiteDerecha = 8.5f;
    public float limiteIzquierda = -8.5f;
    public float limiteArriba = 4.8f;
    public float limiteAbajo = -3.5f;

    [Header("Sistema de Batería / Energía")]
    public float bateriaActual = 100f;
    public float bateriaMax = 100f;
    public float consumoPorSegundo = 0.5f;
    public TextMeshProUGUI textoBateria;

    [Header("Panel de Game Over")]
    public GameObject panelGameOver;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Time.timeScale = 1f; // SEGURIDAD: Forzar que el juego comience corriendo al cargar la escena
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bateriaActual = bateriaMax;

        if (panelGameOver != null) panelGameOver.SetActive(false); // Asegurar que inicie oculto
    }

    void Update()
    {
        // Si el juego está pausado, no procesar movimiento ni desgaste
        if (Time.timeScale == 0f) return;

        // 1. Movimiento
        float movimientoX = Input.GetAxisRaw("Horizontal");
        float movimientoY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(movimientoX * velocidad, movimientoY * velocidad);

        // 2. Giro de 180 grados
        if (movimientoX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movimientoX < 0)
        {
            spriteRenderer.flipX = true;
        }

        // 3. Límites de pantalla
        Vector3 posicion = transform.position;
        posicion.x = Mathf.Clamp(posicion.x, limiteIzquierda, limiteDerecha);
        posicion.y = Mathf.Clamp(posicion.y, limiteAbajo, limiteArriba);
        transform.position = posicion;

        // 4. Desgaste de Batería con el tiempo
        if (bateriaActual > 0)
        {
            bateriaActual -= consumoPorSegundo * Time.deltaTime;

            if (bateriaActual < 0) bateriaActual = 0;

            // Actualizar texto en pantalla
            if (textoBateria != null)
            {
                textoBateria.text = Mathf.Ceil(bateriaActual).ToString();
            }
        }

        // Cuando la batería se agota (<= 0)
        if (bateriaActual <= 0)
        {
            bateriaActual = 0;
            Debug.Log("¡Batería agotada! Game Over");

            // Activar panel de Game Over y pausar el juego
            if (panelGameOver != null)
            {
                panelGameOver.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}