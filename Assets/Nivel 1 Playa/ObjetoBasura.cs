using UnityEngine;

public class ObjetoBasura : MonoBehaviour
{
    [Header("Tipo de Basura")]
    public string tipoBasura; // "Orgánico", "Inorgánico", "NoReciclable"

    [HideInInspector]
    public bool estaAtrapada = false;
    private Transform transformPinza;
    private Collider2D miCollider;

    public static bool pinzaOcupada = false;

    void Start()
    {
        GameObject pinzaObj = GameObject.Find("PuntoPinza");
        if (pinzaObj != null)
        {
            transformPinza = pinzaObj.transform;
        }
        miCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Si el tiempo se acabó, soltar la basura automáticamente
        ControladorTiempo tiempoControl = FindFirstObjectByType<ControladorTiempo>();
        if (tiempoControl != null && tiempoControl.TiempoAgotado())
        {
            if (estaAtrapada) SoltarBasura();
            return;
        }

        // Si ya está atrapada, se mueve con la pinza SÓLO SI SE MANTIENE PRESIONADA LA TECLA ESPACIO
        if (estaAtrapada && transformPinza != null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position = transformPinza.position;
            }
            else
            {
                // Si el jugador suelta la barra espaciadora, la suelta en la arena
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
                ControladorTiempo tiempoControl = FindFirstObjectByType<ControladorTiempo>();
                if (tiempoControl != null && tiempoControl.TiempoAgotado()) return;

                estaAtrapada = true;
                pinzaOcupada = true;

                if (miCollider != null) miCollider.enabled = false; // Desactiva colisión temporal
            }
        }
    }

    public void SoltarBasura()
    {
        estaAtrapada = false;
        pinzaOcupada = false;
        if (miCollider != null) miCollider.enabled = true;
    }

    public void DepositarEnContenedor()
    {
        pinzaOcupada = false;
        Destroy(gameObject);
    }
}