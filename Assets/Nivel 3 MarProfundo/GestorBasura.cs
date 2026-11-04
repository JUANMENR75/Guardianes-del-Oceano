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
    public float velocidadCaida = 1f;
    public float tiempoMaximoVida = 10f;

    [Header("Retraso entre cada basura del grupo")]
    public float retrasoEntreBasuras = 0.4f; // Milisegundos/segundos de diferencia entre la aparición de una y otra

    [Header("Cantidad de Basura Simultánea")]
    [Range(2, 3)] public int cantidadBasuraSimultanea = 2; // Cantidad de basuras juntas (2 o 3)

    private float temporizadorAparicion;
    private float temporizadorVida;
    private List<GameObject> basurasActivasEnEscena = new List<GameObject>();

    [Header("Límites Estrictos de la Pantalla")]
    public float limiteDerecha = 8.5f;
    public float limiteIzquierda = -8.5f;
    public float limiteArriba = 4.3f;
    public float limiteAbajo = -4f;

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
            if (basurasActivasEnEscena.Count == 0)
            {
                temporizadorAparicion -= Time.deltaTime;
                if (temporizadorAparicion <= 0f)
                {
                    StartCoroutine(AparecerGrupoBasuraCorrutina());
                }
            }
            else
            {
                // Mueve todas las basuras activas hacia abajo
                for (int i = 0; i < basurasActivasEnEscena.Count; i++)
                {
                    GameObject basura = basurasActivasEnEscena[i];
                    if (basura != null && basura.activeSelf)
                    {
                        basura.transform.Translate(Vector3.down * velocidadCaida * Time.deltaTime);

                        Vector3 pos = basura.transform.position;
                        if (pos.y <= limiteAbajo)
                        {
                            pos.y = limiteAbajo;
                            basura.transform.position = pos;
                        }
                    }
                }

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

    IEnumerator AparecerGrupoBasuraCorrutina()
    {
        if (listaDeBasura.Length == 0) yield break;

        basurasActivasEnEscena.Clear();
        int cantidadAGenerar = Mathf.Clamp(cantidadBasuraSimultanea, 1, listaDeBasura.Length);

        // Lista temporal para escoger basuras únicas sin repetir en este grupo
        List<GameObject> disponibles = new List<GameObject>(listaDeBasura);

        // Dividimos el espacio horizontal estrictamente dentro de los límites para garantizar separación
        float margenSeguro = 1f;
        float anchoUtil = (limiteDerecha - margenSeguro) - (limiteIzquierda + margenSeguro);
        float tamSeccion = anchoUtil / cantidadAGenerar;

        for (int i = 0; i < cantidadAGenerar; i++)
        {
            if (disponibles.Count == 0) break;

            int indiceAleatorio = Random.Range(0, disponibles.Count);
            GameObject basuraElegida = disponibles[indiceAleatorio];
            disponibles.RemoveAt(indiceAleatorio); // Evita que se repita en este mismo grupo

            if (basuraElegida != null)
            {
                // Asignamos una sección específica a cada pieza para que aparezcan separadas horizontalmente
                float minX = (limiteIzquierda + margenSeguro) + (i * tamSeccion);
                float maxX = minX + tamSeccion;
                float posX = Random.Range(minX + 0.2f, maxX - 0.2f);

                // Control estricto para que jamás rebase los límites del submarino
                posX = Mathf.Clamp(posX, limiteIzquierda + 0.5f, limiteDerecha - 0.5f);

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
            }

            // Esperar unos instantes antes de hacer aparecer la siguiente basura del grupo
            if (i < cantidadAGenerar - 1)
            {
                yield return new WaitForSeconds(retrasoEntreBasuras);
            }
        }

        temporizadorVida = tiempoMaximoVida;
    }

    public void RecogerBasura(GameObject basuraRecogida, int puntos)
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

        if (puntosTotalesJuego >= 100)
        {
            puntosTotalesJuego = 100;
            if (textoPuntosUI != null) textoPuntosUI.text = "100";

            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        // Ocultar solo la basura que se recolectó
        SpriteRenderer sr = basuraRecogida.GetComponent<SpriteRenderer>();
        Collider2D col = basuraRecogida.GetComponent<Collider2D>();
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        basurasActivasEnEscena.Remove(basuraRecogida);

        // Si ya se recogieron todas las del grupo actual, se prepara el siguiente ciclo
        if (basurasActivasEnEscena.Count == 0)
        {
            temporizadorAparicion = intervaloAparicion;
        }
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
    }
}