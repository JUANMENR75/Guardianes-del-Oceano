using UnityEngine;

public class ControlPila : MonoBehaviour
{
    [Header("Configuración de Recarga y Tiempos")]
    public float puntosRecarga = 10f;
    public bool esPilaRoja = false; // <-- Márcalo en el Inspector si es la pila roja para que reste
    public float intervaloAparicion = 30f;
    public float tiempoEnPantalla = 10f;
    public float velocidadCaida = 0.8f;   // <--- Bajada mucho más lenta y suave

    private float temporizadorAparicion;
    private float temporizadorVida;
    private bool estaActiva = false;
    private Vector2 direccionPila;

    [Header("Límites Estrictos de la Pantalla")]
    public float limiteDerecha = 8.0f;
    public float limiteIzquierda = -8.0f;
    public float limiteArriba = 4.2f;
    public float limiteAbajo = -3.2f;

    private SpriteRenderer spriteRenderer;
    private Collider2D colisionador;
    private MovimientoSubmarino submarino;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisionador = GetComponent<Collider2D>();
        submarino = FindAnyObjectByType<MovimientoSubmarino>();

        OcultarPila();
        temporizadorAparicion = intervaloAparicion;
    }

    void Update()
    {
        if (submarino == null) return;

        if (submarino.bateriaActual <= 80f)
        {
            if (!estaActiva)
            {
                temporizadorAparicion -= Time.deltaTime;
                if (temporizadorAparicion <= 0f)
                {
                    AparecerPila();
                }
            }
            else
            {
                // Movimiento de caída lento
                transform.Translate(direccionPila * velocidadCaida * Time.deltaTime);

                Vector3 pos = transform.position;

                // 1. REBOTE SOLO EN LOS MÁRGENES LATERALES
                if (pos.x <= limiteIzquierda || pos.x >= limiteDerecha)
                {
                    direccionPila.x = -direccionPila.x;
                    pos.x = Mathf.Clamp(pos.x, limiteIzquierda, limiteDerecha);
                }

                // 2. REBOTE EN EL TOPE SUPERIOR
                if (pos.y >= limiteArriba)
                {
                    direccionPila.y = -direccionPila.y;
                    pos.y = Mathf.Clamp(pos.y, limiteAbajo, limiteArriba);
                }

                // 3. SI LLEGA AL FONDO (LÍMITE ABAJO): SE DETIENE POR COMPLETO Y SE QUEDA QUIETA (CERO REBOTE)
                if (pos.y <= limiteAbajo)
                {
                    pos.y = limiteAbajo;
                    direccionPila = Vector2.zero;
                }

                transform.position = pos;

                // Control de tiempo en pantalla
                temporizadorVida -= Time.deltaTime;
                if (temporizadorVida <= 0f)
                {
                    OcultarPila();
                    temporizadorAparicion = intervaloAparicion;
                }
            }
        }
        else
        {
            if (estaActiva) OcultarPila();
            temporizadorAparicion = intervaloAparicion;
        }
    }

    void AparecerPila()
    {
        float randomX = Random.Range(limiteIzquierda + 1f, limiteDerecha - 1f);
        transform.position = new Vector3(randomX, limiteArriba, 0f);

        // Dirección inicial suave hacia abajo con un leve toque horizontal
        direccionPila = new Vector2(Random.Range(-0.2f, 0.2f), -1f).normalized;

        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (colisionador != null) colisionador.enabled = true;

        estaActiva = true;
        temporizadorVida = tiempoEnPantalla;
    }

    void OcultarPila()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (colisionador != null) colisionador.enabled = false;
        estaActiva = false;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (!estaActiva) return;

        MovimientoSubmarino sub = otro.GetComponent<MovimientoSubmarino>();
        if (sub != null)
        {
            if (sub.bateriaActual <= 80f)
            {
                if (esPilaRoja)
                {
                    sub.bateriaActual -= puntosRecarga;
                    if (sub.bateriaActual < 0f) sub.bateriaActual = 0f; // Evitar valores negativos extremos
                }
                else
                {
                    sub.bateriaActual += puntosRecarga;
                    if (sub.bateriaActual > 100f) sub.bateriaActual = 100f;
                }

                OcultarPila();
                temporizadorAparicion = intervaloAparicion;
            }
        }
    }
}