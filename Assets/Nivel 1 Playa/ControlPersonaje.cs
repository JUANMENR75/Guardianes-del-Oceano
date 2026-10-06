using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    private Rigidbody2D rb;
    private Vector2 movimiento;

    [Header("Sprites del Personaje")]
    public Sprite spriteNormal;   // Imagen normal caminando
    public Sprite spritePinza;    // Imagen con la pinza estirada
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Seguridad para evitar que se caiga o rote
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        // 1. Capturar movimiento con las flechas o W, A, S, D
        float moverX = Input.GetAxisRaw("Horizontal");
        float moverY = Input.GetAxisRaw("Vertical");
        movimiento = new Vector2(moverX, moverY).normalized;

        // 2. Mecánica de la barra espaciadora para la pinza
        if (Input.GetKey(KeyCode.Space))
        {
            if (spritePinza != null)
            {
                spriteRenderer.sprite = spritePinza;
            }
        }
        else
        {
            if (spriteNormal != null)
            {
                spriteRenderer.sprite = spriteNormal;
            }
        }
    }

    void FixedUpdate()
    {
        // 3. Mover físicamente al personaje por la pantalla
        if (rb != null)
        {
            rb.MovePosition(rb.position + movimiento * velocidad * Time.fixedDeltaTime);
        }
    }

    // Función para cuando deposita la basura correctamente
    public void DepositarBasuraCorrecta()
    {
        if (spriteRenderer != null && spriteNormal != null)
        {
            spriteRenderer.sprite = spriteNormal;
        }
    }
}