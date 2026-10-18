using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movimiento;
    private CircleCollider2D colisionadorPinza;

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

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        // 1. Control automático del colisionador de la pinza:
        // Solo se enciende al mantener presionada la barra espaciadora para evitar capturas anticipadas.
        if (colisionadorPinza != null)
        {
            colisionadorPinza.enabled = Input.GetKey(KeyCode.Space);
        }

        // 2. Si presionamos espacio y tenemos basura detectada en la pinza, la recogemos
        if (Input.GetKey(KeyCode.Space) && basuraDetectada != null && !tieneBasura)
        {
            RecogerBasura();
        }

        // 3. Control de Animaciones (Pinza, Caminando, Quieto)
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("pinza", true);
            anim.SetBool("caminando", false);
            movimiento = Vector2.zero; // Se detiene al usar la pinza para atrapar
        }
        else
        {
            anim.SetBool("pinza", false);

            float moverX = Input.GetAxisRaw("Horizontal");
            float moverY = Input.GetAxisRaw("Vertical");
            movimiento = new Vector2(moverX, moverY).normalized;

            if (moverX != 0 || moverY != 0)
            {
                anim.SetBool("caminando", true);
            }
            else
            {
                anim.SetBool("caminando", false);
            }
        }
    }

    void FixedUpdate()
    {
        // 4. Movimiento físico respetando los límites de la arena
        if (rb != null)
        {
            Vector2 nuevaPosicion = rb.position + movimiento * velocidad * Time.fixedDeltaTime;

            nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, minX, maxX);
            nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, minY, maxY);

            rb.MovePosition(nuevaPosicion);
        }

        // 5. BLOQUEO ABSOLUTO DE ESCALA: Mantiene siempre el tamaño exacto sin alterarse
        transform.localScale = escalaUnica;
    }

    void RecogerBasura()
    {
        tieneBasura = true;
        basuraEnMano = basuraDetectada;

        // Desactivar físicas de la basura para que viaje con el personaje sin caerse
        Rigidbody2D rbBasura = basuraEnMano.GetComponent<Rigidbody2D>();
        if (rbBasura != null) rbBasura.simulated = false;

        // La basura se vuelve hija del personaje para transportarla frente a la pinza
        basuraEnMano.transform.SetParent(transform);
        basuraEnMano.transform.localPosition = new Vector3(1.2f, 0f, 0f); // Posición al frente en la mano

        Debug.Log("¡Basura recogida con la pinza!");
    }

    // Detección mediante Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Detectar la basura cercana cuando la pinza está activa
        if ((collision.CompareTag("Basura")|| collision.CompareTag("Organico") || collision.CompareTag("Inorganico") || collision.CompareTag("NoReciclable")) && !tieneBasura)
        {
            basuraDetectada = collision.gameObject;
        }

        // 2. Depositar la basura al tocar el contenedor correcto
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
                    // ¡Contenedor correcto! Sumamos puntos y destruimos la basura
                    ContenedorBasura.totalBasuraReciclada++;
                    ContenedorBasura.totalPuntos += 10;

                    objBasura.DepositerEnContenedorLocal(); // Destruye la basura

                    Debug.Log("¡Depósito correcto en el contenedor!");

                    // Reiniciar estados para permitir recoger más basura
                    tieneBasura = false;
                    basuraDetectada = null;
                    basuraEnMano = null;
                }
                else
                {
                    Debug.Log("¡Contenedor incorrecto! Este contenedor es para: " + contenedor.tipoBasuraAceptada);
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