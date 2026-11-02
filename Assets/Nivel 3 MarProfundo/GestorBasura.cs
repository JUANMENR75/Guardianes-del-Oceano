using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GestorBasura : MonoBehaviour
{
    [Header("Objetos de Basura en la Jerarquía")]
    public GameObject[] listaDeBasura;

    [Header("Configuración de Tiempos y Velocidad")]
    public float intervaloAparicion = 3f;
    public float velocidadCaida = 1f;
    public float tiempoMaximoVida = 8f;

    private float temporizadorAparicion;
    private float temporizadorVida;
    private GameObject basuraActualEnEscena;
    private int puntosAleatoriosObjeto = 0;
    private Vector2 direccionBasura;

    [Header("Límites Estrictos de la Pantalla")]
    public float limiteDerecha = 8.5f;
    public float limiteIzquierda = -8.5f;
    public float limiteArriba = 4.3f;
    public float limiteAbajo = -4f;

    [Header("Referencias de Interfaz (UI)")]
    public TextMeshProUGUI textoPuntosUI;
    public TextMeshProUGUI textoBasuraUI;

    [Header("Panel de Victoria")]
    public GameObject panelVictoria;        // Arrastra aquí tu PanelVictoria del Canvas

    public static int puntosTotalesJuego = 0;
    public static int cantidadBasuraTotal = 0;

    private MovimientoSubmarino submarino;

    void Start()
    {
        submarino = FindAnyObjectByType<MovimientoSubmarino>();
        OcultarTodaLaBasura();

        if (panelVictoria != null) panelVictoria.SetActive(false); // Ocultar al iniciar

        temporizadorAparicion = intervaloAparicion;
        puntosTotalesJuego = 0;
        cantidadBasuraTotal = 0;
    }

    void Update()
    {
        if (submarino == null) return;

        if (submarino.bateriaActual <= 97f)
        {
            if (basuraActualEnEscena == null)
            {
                temporizadorAparicion -= Time.deltaTime;
                if (temporizadorAparicion <= 0f)
                {
                    AparecerBasura();
                }
            }
            else
            {
                basuraActualEnEscena.transform.Translate(direccionBasura * velocidadCaida * Time.deltaTime);

                Vector3 pos = basuraActualEnEscena.transform.position;

                if (pos.x <= limiteIzquierda || pos.x >= limiteDerecha)
                {
                    direccionBasura.x = -direccionBasura.x;
                    pos.x = Mathf.Clamp(pos.x, limiteIzquierda, limiteDerecha);
                }

                if (pos.y >= limiteArriba)
                {
                    direccionBasura.y = -direccionBasura.y;
                    pos.y = Mathf.Clamp(pos.y, limiteAbajo, limiteArriba);
                }

                if (pos.y <= limiteAbajo)
                {
                    pos.y = limiteAbajo;
                    direccionBasura = Vector2.zero;
                }

                basuraActualEnEscena.transform.position = pos;

                temporizadorVida -= Time.deltaTime;
                if (temporizadorVida <= 0f)
                {
                    OcultarTodaLaBasura();
                    temporizadorAparicion = intervaloAparicion;
                }
            }
        }
        else
        {
            OcultarTodaLaBasura();
            temporizadorAparicion = intervaloAparicion;
        }
    }

    void AparecerBasura()
    {
        if (listaDeBasura.Length == 0) return;

        int indiceAleatorio = Random.Range(0, listaDeBasura.Length);
        basuraActualEnEscena = listaDeBasura[indiceAleatorio];

        if (basuraActualEnEscena != null)
        {
            float randomX = Random.Range(limiteIzquierda + 1f, limiteDerecha - 1f);
            basuraActualEnEscena.transform.position = new Vector3(randomX, limiteArriba, 0f);

            direccionBasura = new Vector2(Random.Range(-0.3f, 0.3f), -1f).normalized;

            SpriteRenderer sr = basuraActualEnEscena.GetComponent<SpriteRenderer>();
            Collider2D col = basuraActualEnEscena.GetComponent<Collider2D>();

            if (sr != null) sr.enabled = true;
            if (col != null) col.enabled = true;

            // PUNTUACIÓN ALEATORIA ENTRE 1 Y 5 (El límite superior en Random.Range es exclusivo, por eso se pone 6)
            puntosAleatoriosObjeto = Random.Range(1, 5);

            DetectarToqueBasura detector = basuraActualEnEscena.GetComponent<DetectarToqueBasura>();
            if (detector == null)
            {
                detector = basuraActualEnEscena.AddComponent<DetectarToqueBasura>();
            }
            detector.gestor = this;
            detector.puntosOtorgados = puntosAleatoriosObjeto;

            temporizadorVida = tiempoMaximoVida;
        }
    }

    public void RecogerBasura(int puntos)
    {
        cantidadBasuraTotal++;
        puntosTotalesJuego += puntos;

        if (textoPuntosUI == null)
        {
            GameObject objPuntos = GameObject.Find("TextPunots");
            if (objPuntos != null) textoPuntosUI = objPuntos.GetComponent<TextMeshProUGUI>();
        }

        if (textoBasuraUI == null)
        {
            GameObject objBasura = GameObject.Find("TextoBasura");
            if (objBasura != null) textoBasuraUI = objBasura.GetComponent<TextMeshProUGUI>();
        }

        if (textoPuntosUI != null) textoPuntosUI.text = puntosTotalesJuego.ToString();
        if (textoBasuraUI != null) textoBasuraUI.text = cantidadBasuraTotal.ToString();

        // CONDICIÓN DE VICTORIA
        if (puntosTotalesJuego >= 100)
        {
            puntosTotalesJuego = 100;
            if (textoPuntosUI != null) textoPuntosUI.text = "100";

            Debug.Log("¡Victoria alcanzada!");

            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);
                Time.timeScale = 0f; // Pausa el juego
            }
        }

        OcultarTodaLaBasura();
        temporizadorAparicion = intervaloAparicion;
    }

    void OcultarTodaLaBasura()
    {
        if (listaDeBasura != null)
        {
            foreach (GameObject obj in listaDeBasura)
            {
                if (obj != null)
                {
                    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                    Collider2D col = obj.GetComponent<Collider2D>();
                    if (sr != null) sr.enabled = false;
                    if (col != null) col.enabled = false;
                }
            }
        }
        basuraActualEnEscena = null;
    }
}