using UnityEngine;

public class ControlPila : MonoBehaviour
{
    [Header("Configuración de Recarga y Tiempos")]
    public float puntosRecarga = 5f;
    public bool esPilaRoja = false;
    public float intervaloAparicion = 20f;
    public float tiempoEnPantalla = 10f;
    public float velocidadCaida = 2f;

    [Header("Límites Estrictos de la Pantalla")]
    public float limiteDerecha = 8.5f;
    public float limiteIzquierda = -8.5f;
    public float limiteArriba = 4.3f;
    public float limiteAbajo = -4f;

    private float temporizadorAparicion;
    private float temporizadorVida;
    private bool estaActiva = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D colisionador;
    private Rigidbody2D rb2d;
    private MovimientoSubmarino submarino;

    public static bool turnoPilaRoja = false;
    public static bool hayAlgunaPilaActiva = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisionador = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        submarino = FindAnyObjectByType<MovimientoSubmarino>();

        if (rb2d != null)
        {
            rb2d.gravityScale = 0f; // Evitamos gravedad extra de Unity
            rb2d.freezeRotation = true;
        }

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
                if (hayAlgunaPilaActiva) return;
                if (esPilaRoja != turnoPilaRoja) return;

                temporizadorAparicion -= Time.deltaTime;
                if (temporizadorAparicion <= 0f)
                {
                    AparecerPila();
                }
            }
            else
            {
                // Forzamos el descenso usando el Rigidbody2D
                if (rb2d != null && rb2d.simulated)
                {
                    rb2d.linearVelocity = new Vector2(0f, -Mathf.Abs(velocidadCaida));
                }

                // Control estricto de límites
                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(pos.x, limiteIzquierda, limiteDerecha);

                if (pos.y <= limiteAbajo)
                {
                    pos.y = limiteAbajo;
                    if (rb2d != null) rb2d.linearVelocity = Vector2.zero;
                }

                transform.position = pos;

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

        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (colisionador != null) colisionador.enabled = true;
        if (rb2d != null) rb2d.simulated = true;

        estaActiva = true;
        hayAlgunaPilaActiva = true;
        temporizadorVida = tiempoEnPantalla;
    }

    void OcultarPila()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (colisionador != null) colisionador.enabled = false;
        if (rb2d != null)
        {
            rb2d.linearVelocity = Vector2.zero;
            rb2d.simulated = false;
        }

        if (estaActiva)
        {
            turnoPilaRoja = !turnoPilaRoja;
        }

        estaActiva = false;
        hayAlgunaPilaActiva = false;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (!estaActiva) return;

        MovimientoSubmarino sub = otro.GetComponent<MovimientoSubmarino>();
        if (sub != null)
        {
            // 1. Desactivamos TODO de inmediato al primer toque para que desaparezca al instante visualmente
            if (spriteRenderer != null) spriteRenderer.enabled = false;
            if (colisionador != null) colisionador.enabled = false;
            if (rb2d != null)
            {
                rb2d.linearVelocity = Vector2.zero;
                rb2d.simulated = false;
            }

            estaActiva = false;
            hayAlgunaPilaActiva = false;
            transform.position = new Vector3(0f, -999f, 0f);

            // 2. Aplicamos la lógica de la batería de forma inmediata
            if (sub.bateriaActual <= 80f)
            {
                if (esPilaRoja)
                {
                    sub.bateriaActual -= puntosRecarga;
                    if (sub.bateriaActual < 0f) sub.bateriaActual = 0f;
                }
                else
                {
                    sub.bateriaActual += puntosRecarga;
                    if (sub.bateriaActual > 100f) sub.bateriaActual = 100f;
                }
            }

            turnoPilaRoja = !turnoPilaRoja;
            temporizadorAparicion = intervaloAparicion;
        }
    }
}