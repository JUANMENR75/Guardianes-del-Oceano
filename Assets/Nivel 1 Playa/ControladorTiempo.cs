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

        GameObject barraObj = GameObject.Find("BarraVerde");
        if (barraObj != null)
        {
            barraVerdeImg = barraObj.GetComponent<Image>();
        }

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
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
        }

        int basuraFinal = ContenedorBasura.totalBasuraReciclada;
        int puntosFinal = ContenedorBasura.totalPuntos;

        // Formato limpio de 2 líneas exactas
        if (textoBasuraTotal != null)
        {
            textoBasuraTotal.text = "BASURA\n<b>" + basuraFinal + "</b>";
        }

        if (textoPuntosTotal != null)
        {
            textoPuntosTotal.text = "PUNTOS\n<b>" + puntosFinal + "</b>";
        }

        Time.timeScale = 0f;
    }

    public bool TiempoAgotado()
    {
        return tiempoTerminado;
    }
}