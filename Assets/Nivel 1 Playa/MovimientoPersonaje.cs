using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movimiento;
    private CircleCollider2D colisionadorPinza;
    private SpriteRenderer spriteRenderer; // <-- Variable para el giro

    [Header("Límites de la Arena")]
    public float minX = -7.8f;
    public float maxX = 7f;
    public float minY = -2.8f;
    public float maxY = 2f;

    [Header("Tamaño Único y Uniforme")]
    [SerializeField] private Vector3 escalaUnica = new Vector3(0.4f, 0.4f, 1f);

    [Header("Sistema de Recolección")]
    private GameObject basuraDetectada = null;
    private GameObject basuraEnMano = null;
    private bool tieneBasura = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colisionadorPinza = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // <-- Inicializamos el SpriteRenderer

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        if (colisionadorPinza != null)
        {
            colisionadorPinza.enabled = Input.GetKey(KeyCode.Space);
        }

        // Sonido de la pinza al presionar espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ControladorSonidoPlaya.Instance != null)
            {
                ControladorSonidoPlaya.Instance.ReproducirSonidoPinza();
            }
        }

        // RECOGER BASURA
        if (Input.GetKey(KeyCode.Space) && basuraDetectada != null && !tieneBasura)
        {
            RecogerBasura();
        }

        // Control de Animaciones y Sonido de Caminata
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("pinza", true);
            anim.SetBool("caminando", false);
            movimiento = Vector2.zero;

            if (ControladorSonidoPlaya.Instance != null)
            {
                ControladorSonidoPlaya.Instance.ControlarCaminata(false);
            }
        }
        else
        {
            anim.SetBool("pinza", false);

            float moverX = Input.GetAxisRaw("Horizontal");
            float moverY = Input.GetAxisRaw("Vertical");
            movimiento = new Vector2(moverX, moverY).normalized;

            // --- VOLTEAR AL PERSONAJE AL ESTILO MARIO ---
            if (moverX > 0)
            {
                if (spriteRenderer != null) spriteRenderer.flipX = false; // Ve a la derecha
            }
            else if (moverX < 0)
            {
                if (spriteRenderer != null) spriteRenderer.flipX = true;  // Ve a la izquierda
            }

            if (moverX != 0 || moverY != 0)
            {
                anim.SetBool("caminando", true);
                if (ControladorSonidoPlaya.Instance != null)
                {
                    ControladorSonidoPlaya.Instance.ControlarCaminata(true);
                }
            }
            else
            {
                anim.SetBool("caminando", false);
                if (ControladorSonidoPlaya.Instance != null)
                {
                    ControladorSonidoPlaya.Instance.ControlarCaminata(false);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            Vector2 nuevaPosicion = rb.position + movimiento * velocidad * Time.fixedDeltaTime;
            nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, minX, maxX);
            nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, minY, maxY);
            rb.MovePosition(nuevaPosicion);
        }

        transform.localScale = escalaUnica;
    }

    void RecogerBasura()
    {
        tieneBasura = true;
        basuraEnMano = basuraDetectada;

        Rigidbody2D rbBasura = basuraEnMano.GetComponent<Rigidbody2D>();
        if (rbBasura != null) rbBasura.simulated = false;

        basuraEnMano.transform.SetParent(transform);
        basuraEnMano.transform.localPosition = new Vector3(1.2f, 0f, 0f);
        Debug.Log("¡Basura recogida con la pinza!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Basura") || collision.CompareTag("Organico") || collision.CompareTag("Inorganico") || collision.CompareTag("NoReciclable")) && !tieneBasura)
        {
            basuraDetectada = collision.gameObject;
        }

        if (collision.CompareTag("Contenedor") && tieneBasura && basuraEnMano != null)
        {
            ContenedorBasura contenedor = collision.GetComponent<ContenedorBasura>();
            ObjetoBasura objBasura = basuraEnMano.GetComponent<ObjetoBasura>();

            if (contenedor != null && objBasura != null)
            {
                string tipoBasuraLimpio = LimpiarTexto(objBasura.tipoBasura);
                string tipoAceptadoLimpio = LimpiarTexto(contenedor.tipoBasuraAceptada);

                if (tipoBasuraLimpio == tipoAceptadoLimpio)
                {
                    ContenedorBasura.totalBasuraReciclada++;
                    ContenedorBasura.totalPuntos += 10;

                    objBasura.DepositerEnContenedorLocal();

                    if (ControladorSonidoPlaya.Instance != null)
                    {
                        ControladorSonidoPlaya.Instance.ReproducirSonidoDepositar();
                    }

                    tieneBasura = false;
                    basuraDetectada = null;
                    basuraEnMano = null;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == basuraDetectada)
        {
            basuraDetectada = null;
        }
    }

    string LimpiarTexto(string texto)
    {
        if (string.IsNullOrEmpty(texto)) return "";
        texto = texto.Trim().ToLower();
        texto = texto.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        return texto;
    }
}