using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorTiempo : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    public float tiempoMaximo = 60f;
    private float tiempoRestante;
    private bool tiempoTerminado = false;

    private Image barraVerdeImg;

    [Header("Panel y Textos de Game Over")]
    public GameObject panelGameOver;
    public TMP_Text textoBasuraTotal;
    public TMP_Text textoPuntosTotal;

    void Start()
    {
        tiempoRestante = tiempoMaximo;

        // Buscar automáticamente la barra verde del tiempo
        GameObject barraObj = GameObject.Find("BarraVerdeTiempo");
        if (barraObj != null)
        {
            barraVerdeImg = barraObj.GetComponent<Image>();
        }

        // Asegurar que el panel de Game Over esté oculto al iniciar
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }
        else
        {
            // Si no está asignado en el Inspector, lo buscamos por nombre
            GameObject panelEncontrado = GameObject.Find("PanelGameOver");
            if (panelEncontrado != null)
            {
                panelGameOver = panelEncontrado;
                panelGameOver.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            if (barraVerdeImg != null)
            {
                barraVerdeImg.fillAmount = tiempoRestante / tiempoMaximo;
            }
        }
        else
        {
            tiempoRestante = 0;
            if (!tiempoTerminado)
            {
                tiempoTerminado = true;
                ActivarGameOver();
            }
        }
    }

    void ActivarGameOver()
    {
        Debug.Log("¡Se acabó el tiempo! Mostrando Game Over.");

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);

            // Búsqueda automática de los textos dentro del panel si las casillas están vacías
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

        // Obtener las cantidades reales recolectadas durante la partida
        int basuraFinal = ContenedorBasura.totalBasuraReciclada;
        int puntosFinal = ContenedorBasura.totalPuntos;

        // Inyectar los valores reales respetando el título
        if (textoBasuraTotal != null)
        {
            textoBasuraTotal.text = "BASURA\n" + basuraFinal;
        }

        if (textoPuntosTotal != null)
        {
            textoPuntosTotal.text = "PUNTOS\n" + puntosFinal;
        }

        Time.timeScale = 0f;
    }

    public bool TiempoAgotado()
    {
        return tiempoTerminado;
    }
}