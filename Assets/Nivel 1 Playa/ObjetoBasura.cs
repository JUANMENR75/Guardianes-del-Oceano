using UnityEngine;

public class ObjetoBasura : MonoBehaviour
{
    [Header("Tipo de Basura")]
    public string tipoBasura; // "Orgánico", "Inorgánico", "NoReciclable"

    [Header("Ajuste Manual de Posición al Transportar")]
    public Vector3 offsetRecogida = new Vector3(1.2f, 0f, 0f); // Ponle un valor pequeño en X (ej. 1.0 o 1.2) y Z en 0

    [HideInInspector]
    public bool estaAtrapada = false;
    private Transform transformPinza;
    private Collider2D miCollider;
    private SpriteRenderer spriteRenderer;

    public static bool pinzaOcupada = false;

    void Start()
    {
        GameObject pinzaObj = GameObject.Find("PuntoPinza");
        if (pinzaObj != null)
        {
            transformPinza = pinzaObj.transform;
        }
        miCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Si el tiempo se acabó, soltar la basura automáticamente
        ControladorTiempo tiempoControl = FindAnyObjectByType<ControladorTiempo>();
        if (tiempoControl != null && tiempoControl.TiempoAgotado())
        {
            if (estaAtrapada) SoltarBasura();
            return;
        }

        // Si ya está atrapada, se mueve con la pinza usando el offset y asegurando que esté al frente
        if (estaAtrapada && transformPinza != null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position = transformPinza.position + offsetRecogida;
            }
            else
            {
                SoltarBasura();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Si toca la pinza Y EL JUGADOR PRESIONA LA BARRA ESPACIADORA, la atrapa
        if (collision.gameObject.name == "PuntoPinza" && !estaAtrapada && !pinzaOcupada)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ControladorTiempo tiempoControl = FindAnyObjectByType<ControladorTiempo>();
                if (tiempoControl != null && tiempoControl.TiempoAgotado()) return;

                estaAtrapada = true;
                pinzaOcupada = true;

                if (miCollider != null) miCollider.enabled = false; // Desactiva colisión temporal

                // FORZAR QUE SE VEA ENFRENTE: Subimos el Order in Layer para que esté sobre el personaje
                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 50;
                }
            }
        }
    }

    public void SoltarBasura()
    {
        estaAtrapada = false;
        pinzaOcupada = false;
        if (miCollider != null) miCollider.enabled = true;

        // Regresar el orden normal cuando se suelta
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 0;
        }
    }

    public void DepositarEnContenedor()
    {
        pinzaOcupada = false;
        Destroy(gameObject);
    }

    public void DepositerEnContenedorLocal()
    {
        pinzaOcupada = false;
        Destroy(gameObject);
    }
}