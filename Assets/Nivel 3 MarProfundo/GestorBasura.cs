using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GestorBasura : MonoBehaviour
{
    [Header("Objetos de Basura en la Jerarquía")]
    public GameObject[] listaDeBasura;

    [Header("Configuración de Tiempos y Velocidad")]
    public float intervaloAparicion = 2f;
    public float velocidadCaida = 2.2f;
    public float tiempoMaximoVida = 11f;

    [Header("Separación en Tiempo y Espacio")]
    public float retrasoMinimoEntreBasuras = 1.5f;
    public float retrasoMaximoEntreBasuras = 3.0f;
    public float separacionMinimaX = 4.0f;

    [Header("Cantidad de Basura Simultánea")]
    [Range(2, 3)] public int cantidadBasuraSimultanea = 2;

    private float temporizadorAparicion;
    private List<GameObject> basurasActivasEnEscena = new List<GameObject>();
    private Dictionary<GameObject, float> tiemposDeVidaIndividuales = new Dictionary<GameObject, float>();
    private bool generandoGrupo = false;

    [Header("Límites Estrictos de la Pantalla")]
    public float limiteDerecha = 8.5f;
    public float limiteIzquierda = -8.5f;
    public float limiteArriba = 4.3f;
    public float limiteAbajo = -3.5f;

    [Header("Referencias de Interfaz (UI)")]
    public TextMeshProUGUI textoPuntosUI;
    public TextMeshProUGUI textoBasuraUI;

    [Header("Panel de Victoria")]
    public GameObject panelVictoria;

    public static int puntosTotalesJuego = 0;
    public static int cantidadBasuraTotal = 0;

    private MovimientoSubmarino submarino;

    void Start()
    {
        submarino = FindAnyObjectByType<MovimientoSubmarino>();
        OcultarTodaLaBasura();

        if (panelVictoria != null) panelVictoria.SetActive(false);

        temporizadorAparicion = intervaloAparicion;
        puntosTotalesJuego = 0;
        cantidadBasuraTotal = 0;
    }

    void Update()
    {
        if (submarino == null) return;

        if (submarino.bateriaActual <= 97f)
        {
            if (basurasActivasEnEscena.Count == 0 && !generandoGrupo)
            {
                temporizadorAparicion -= Time.deltaTime;
                if (temporizadorAparicion <= 0f)
                {
                    StartCoroutine(AparecerGrupoBasuraCorrutina());
                }
            }

            // Mueve y controla el tiempo de vida individual de cada basura activa
            for (int i = basurasActivasEnEscena.Count - 1; i >= 0; i--)
            {
                GameObject basura = basurasActivasEnEscena[i];
                if (basura != null)
                {
                    basura.transform.Translate(Vector3.down * velocidadCaida * Time.deltaTime, Space.World);

                    Vector3 pos = basura.transform.position;
                    pos.x = Mathf.Clamp(pos.x, limiteIzquierda + 1.2f, limiteDerecha - 1.2f);
                    pos.y = Mathf.Clamp(pos.y, limiteAbajo, limiteArriba);
                    basura.transform.position = pos;

                    if (tiemposDeVidaIndividuales.ContainsKey(basura))
                    {
                        tiemposDeVidaIndividuales[basura] -= Time.deltaTime;
                        if (tiemposDeVidaIndividuales[basura] <= 0f)
                        {
                            OcultarUnaBasura(basura);
                            if (basurasActivasEnEscena.Count == 0 && !generandoGrupo)
                            {
                                temporizadorAparicion = intervaloAparicion;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            StopAllCoroutines();
            generandoGrupo = false;
            OcultarTodaLaBasura();
            temporizadorAparicion = intervaloAparicion;
        }
    }

    IEnumerator AparecerGrupoBasuraCorrutina()
    {
        if (listaDeBasura.Length == 0) yield break;

        generandoGrupo = true;
        int cantidadAGenerar = Mathf.Clamp(cantidadBasuraSimultanea, 1, listaDeBasura.Length);
        List<GameObject> disponibles = new List<GameObject>(listaDeBasura);

        float minX = limiteIzquierda + 1.2f;
        float maxX = limiteDerecha - 1.2f;

        for (int i = 0; i < cantidadAGenerar; i++)
        {
            if (disponibles.Count == 0) break;

            int indiceAleatorio = Random.Range(0, disponibles.Count);
            GameObject basuraElegida = disponibles[indiceAleatorio];
            disponibles.RemoveAt(indiceAleatorio);

            if (basuraElegida != null)
            {
                float posX = ObtenerPosicionXSeparada(minX, maxX);
                basuraElegida.transform.position = new Vector3(posX, limiteArriba, 0f);

                SpriteRenderer sr = basuraElegida.GetComponent<SpriteRenderer>();
                Collider2D col = basuraElegida.GetComponent<Collider2D>();

                if (sr != null) sr.enabled = true;
                if (col != null) col.enabled = true;

                int puntosAleatorios = Random.Range(1, 5);

                DetectarToqueBasura detector = basuraElegida.GetComponent<DetectarToqueBasura>();
                if (detector == null)
                {
                    detector = basuraElegida.AddComponent<DetectarToqueBasura>();
                }
                detector.gestor = this;
                detector.puntosOtorgados = puntosAleatorios;

                basurasActivasEnEscena.Add(basuraElegida);
                tiemposDeVidaIndividuales[basuraElegida] = tiempoMaximoVida;
            }

            // Esperar un tiempo aleatorio diferente antes de soltar la siguiente basura
            if (i < cantidadAGenerar - 1)
            {
                float espera = Random.Range(retrasoMinimoEntreBasuras, retrasoMaximoEntreBasuras);
                yield return new WaitForSeconds(espera);
            }
        }

        generandoGrupo = false;
    }

    float ObtenerPosicionXSeparada(float minX, float maxX)
    {
        float mejorX = Random.Range(minX, maxX);
        float mayorDistancia = -1f;

        // Busca una coordenada X bien separada de cualquier basura que siga en pantalla
        for (int intento = 0; intento < 25; intento++)
        {
            float candidataX = Random.Range(minX, maxX);
            float distanciaMinimaActual = float.MaxValue;

            foreach (GameObject activa in basurasActivasEnEscena)
            {
                if (activa != null)
                {
                    float dist = Mathf.Abs(candidataX - activa.transform.position.x);
                    if (dist < distanciaMinimaActual)
                    {
                        distanciaMinimaActual = dist;
                    }
                }
            }

            if (distanciaMinimaActual >= separacionMinimaX)
            {
                return candidataX;
            }

            if (distanciaMinimaActual > mayorDistancia)
            {
                mayorDistancia = distanciaMinimaActual;
                mejorX = candidataX;
            }
        }

        return mejorX;
    }

    public void RecogerBasura(GameObject basuraRecogida, int puntos)
    {
        cantidadBasuraTotal++;
        puntosTotalesJuego += puntos;

        if (ControladorSonidoMarProfundo.Instance != null)
        {
            ControladorSonidoMarProfundo.Instance.ReproducirSonidoBasura();
        }

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

        if (puntosTotalesJuego >= 100)
        {
            puntosTotalesJuego = 100;
            if (textoPuntosUI != null) textoPuntosUI.text = "100";

            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);
                Time.timeScale = 0f;

                if (ControladorSonidoMarProfundo.Instance != null)
                {
                    ControladorSonidoMarProfundo.Instance.ActivarSonidoVictoria();
                }
            }
        }

        OcultarUnaBasura(basuraRecogida);

        if (basurasActivasEnEscena.Count == 0 && !generandoGrupo)
        {
            temporizadorAparicion = intervaloAparicion;
        }
    }

    void OcultarUnaBasura(GameObject basura)
    {
        if (basura == null) return;

        SpriteRenderer sr = basura.GetComponent<SpriteRenderer>();
        Collider2D col = basura.GetComponent<Collider2D>();
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        basurasActivasEnEscena.Remove(basura);
        tiemposDeVidaIndividuales.Remove(basura);
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
        basurasActivasEnEscena.Clear();
        tiemposDeVidaIndividuales.Clear();
    }
}