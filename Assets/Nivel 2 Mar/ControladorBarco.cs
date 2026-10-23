using UnityEngine;

public class ControladorBarco : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadMovimiento = 2f;

    [Header("Límites de Pantalla")]
    public float limiteSuperior = -0.8f;
    public float limiteInferior = -7.6f;

    [Header("Configuración de Balanceo")]
    public float amplitudRotacion = 6.58f;
    public float velocidadRotacion = 2f;

    private float posicionBaseY;

    [Header("Referencias del Sistema")]
    public GestorPuntuacion gestorPuntuacion;
    public GestorGameOver gestorGameOver;

    void Start()
    {
        posicionBaseY = transform.position.y;

        if (gestorPuntuacion == null)
        {
            gestorPuntuacion = FindAnyObjectByType<GestorPuntuacion>();
        }

        if (gestorGameOver == null)
        {
            gestorGameOver = FindAnyObjectByType<GestorGameOver>();
        }
    }

    void Update()
    {
        float inputY = Input.GetAxisRaw("Vertical");
        posicionBaseY += inputY * velocidadMovimiento * Time.deltaTime;
        posicionBaseY = Mathf.Clamp(posicionBaseY, limiteInferior, limiteSuperior);

        float rotacionZ = Mathf.Sin(Time.time * velocidadRotacion) * amplitudRotacion;

        transform.position = new Vector3(transform.position.x, posicionBaseY, 0);
        transform.rotation = Quaternion.Euler(0, 0, rotacionZ);
    }

    // Detección por Trigger para Monedas, Basura y Bombas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Moneda"))
        {
            if (gestorPuntuacion != null)
                gestorPuntuacion.SumarMoneda(1);

            if (SonidosEscena1.instance != null)
                SonidosEscena1.instance.ReproducirSonidoMoneda();

            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Basura"))
        {
            if (gestorPuntuacion != null)
                gestorPuntuacion.SumarBasura(1);

            if (SonidosEscena1.instance != null)
                SonidosEscena1.instance.ReproducirSonidoBasura();

            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Bomba"))
        {
            Debug.Log("¡BOMBA TOCADA! Activando pantalla de Game Over...");

            if (SonidosEscena1.instance != null)
            {
                SonidosEscena1.instance.TriggerGameOver();
            }

            if (gestorGameOver != null)
            {
                gestorGameOver.ActivarGameOver();
            }
            else
            {
                GestorGameOver gameOverEmergencia = FindAnyObjectByType<GestorGameOver>();
                if (gameOverEmergencia != null)
                {
                    gameOverEmergencia.ActivarGameOver();
                }
            }
        }
    }
}