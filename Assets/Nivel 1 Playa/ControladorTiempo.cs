using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorTiempo : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    public float tiempoMaximo = 90f;
    private float tiempoRestante;
    private bool tiempoTerminado = false;
    private bool juegoTerminado = false;

    private Image barraVerdeImg;

    [Header("Panel y Textos de Game Over")]
    public GameObject panelGameOver;
    public TMP_Text textoBasuraTotal;
    public TMP_Text textoPuntosTotal;

    [Header("Panel y Textos de Victoria")]
    public GameObject panelVictoria;
    public int metaBasuraVictoria = 35;
    public TMP_Text textoBasuraVictoria;
    public TMP_Text textoPuntosVictoria;

    void Start()
    {
        tiempoRestante = tiempoMaximo;

        GameObject barraObj = GameObject.Find("BarraVerdeTiempo");
        if (barraObj != null)
        {
            barraVerdeImg = barraObj.GetComponent<Image>();
        }

        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            if (barraVerdeImg != null)
            {
                barraVerdeImg.fillAmount = tiempoRestante / tiempoMaximo;
            }

            if (ContenedorBasura.totalBasuraReciclada >= metaBasuraVictoria)
            {
                juegoTerminado = true;
                ActivarVictoriaCompleta();
                return;
            }
        }
        else
        {
            tiempoRestante = 0;
            if (!tiempoTerminado)
            {
                tiempoTerminado = true;
                juegoTerminado = true;
                ActivarGameOver();
            }
        }
    }

    void ActivarVictoriaCompleta()
    {
        Debug.Log("¡Misión cumplida! Limpiando toda la basura y mostrando Panel de Victoria.");

        // 1. DESTRUIR ABSOLUTAMENTE TODA LA BASURA EN PANTALLA
        ObjetoBasura[] basurasPorScript = Object.FindObjectsByType<ObjetoBasura>(FindObjectsSortMode.None);
        foreach (ObjetoBasura b in basurasPorScript)
        {
            if (b != null)
            {
                Destroy(b.gameObject);
            }
        }

        string[] tagsBasura = { "Inorganico", "NoReciclable", "Organico", "Basura" };
        foreach (string t in tagsBasura)
        {
            GameObject[] objetosConTag = GameObject.FindGameObjectsWithTag(t);
            foreach (GameObject obj in objetosConTag)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }

        GeneradorBasura generador = Object.FindAnyObjectByType<GeneradorBasura>();
        if (generador != null)
        {
            generador.gameObject.SetActive(false);
        }

        // 2. CONGELAR Y ASEGURAR QUE LA BARRA VERDE SE QUEDE EN SU VALOR ACTUAL
        if (barraVerdeImg != null)
        {
            barraVerdeImg.fillAmount = tiempoRestante / tiempoMaximo;
            barraVerdeImg.gameObject.SetActive(true);
        }

        // 3. Activar el panel visual de victoria
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);

            if (textoBasuraVictoria == null)
            {
                Transform tBasura = panelVictoria.transform.Find("Text Basura");
                if (tBasura != null) textoBasuraVictoria = tBasura.GetComponent<TMP_Text>();
            }

            if (textoPuntosVictoria == null)
            {
                Transform tPuntos = panelVictoria.transform.Find("TextPuntos");
                if (tPuntos != null) textoPuntosVictoria = tPuntos.GetComponent<TMP_Text>();
            }
        }

        int basuraFinal = ContenedorBasura.totalBasuraReciclada;
        int puntosFinal = ContenedorBasura.totalPuntos;

        if (textoBasuraVictoria != null) textoBasuraVictoria.text = "BASURA\n" + basuraFinal;
        if (textoPuntosVictoria != null) textoPuntosVictoria.text = "PUNTOS\n" + puntosFinal;

        Time.timeScale = 0f;
    }

    void ActivarGameOver()
    {
        Debug.Log("¡Se acabó el tiempo! Mostrando Game Over.");

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);

            if (textoBasuraTotal == null)
            {
                Transform tBasura = panelGameOver.transform.Find("Text Basura");
                if (tBasura != null) textoBasuraTotal = tBasura.GetComponent<TMP_Text>();
            }

            if (textoPuntosTotal == null)
            {
                Transform tPuntos = panelGameOver.transform.Find("TextPuntos");
                if (tPuntos != null) textoPuntosTotal = tPuntos.GetComponent<TMP_Text>();
            }
        }

        int basuraFinal = ContenedorBasura.totalBasuraReciclada;
        int puntosFinal = ContenedorBasura.totalPuntos;

        if (textoBasuraTotal != null) textoBasuraTotal.text = "BASURA\n" + basuraFinal;
        if (textoPuntosTotal != null) textoPuntosTotal.text = "PUNTOS\n" + puntosFinal;

        Time.timeScale = 0f;
    }

    public bool TiempoAgotado()
    {
        return tiempoTerminado;
    }
}