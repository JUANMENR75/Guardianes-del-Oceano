using UnityEngine;
using TMPro;

public class SelectorEscenarios : MonoBehaviour
{
    [Header("Los 2 Escenarios Disponibles")]
    public GameObject objetoPlaya;
    public GameObject objetoOceano;

    [Header("El Texto Central de Títulos")]
    public TextMeshProUGUI textoTitulo;

    private int indiceActual = 0;
    private int totalEscenarios = 2;

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
        if (objetoPlaya != null) objetoPlaya.SetActive(indiceActual == 0);
        if (objetoOceano != null) objetoOceano.SetActive(indiceActual == 1);

        if (textoTitulo != null)
        {
            if (indiceActual == 0)
            {
                textoTitulo.text = "PLAYA";
            }
            else if (indiceActual == 1)
            {
                textoTitulo.text = "MAR";
            }
        }
    }
}