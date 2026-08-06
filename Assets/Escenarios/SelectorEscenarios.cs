using UnityEngine;
using TMPro;

public class SelectorEscenarios : MonoBehaviour
{
    [Header("Los 3 Cuadrados de los Escenarios")]
    public GameObject objetoPlaya;
    public GameObject objetoOceano;
    public GameObject objetoZonaIndustrial;

    [Header("El Texto Central de Títulos")]
    public TextMeshProUGUI textoTitulo;

    private int indiceActual = 0;
    private int totalEscenarios = 3;

    void Start()
    {
        ActualizarVisualizacion();
    }

    public void SiguienteEscenario()
    {
        indiceActual++;
        if (indiceActual >= totalEscenarios)
        {
            indiceActual = 0;
        }
        ActualizarVisualizacion();
    }

    public void AnteriorEscenario()
    {
        indiceActual--;
        if (indiceActual < 0)
        {
            indiceActual = totalEscenarios - 1;
        }
        ActualizarVisualizacion();
    }

    void ActualizarVisualizacion()
    {
        // Activa solo el cuadrado correspondiente y oculta los otros dos
        if (objetoPlaya != null) objetoPlaya.SetActive(indiceActual == 0);
        if (objetoOceano != null) objetoOceano.SetActive(indiceActual == 1);
        if (objetoZonaIndustrial != null) objetoZonaIndustrial.SetActive(indiceActual == 2);

        // Cambia el texto inferior de forma limpia y exacta
        if (textoTitulo != null)
        {
            if (indiceActual == 0)
            {
                textoTitulo.text = "PLAYA";
            }
            else if (indiceActual == 1)
            {
                textoTitulo.text = "OCÉANO";
            }
            else if (indiceActual == 2)
            {
                textoTitulo.text = "ZONA INDUSTRIAL";
            }
        }
    }
}