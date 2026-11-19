using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorEscenarios : MonoBehaviour
{
    [Header("Los 3 Escenarios Disponibles")]
    public GameObject objetoPlaya;
    public GameObject objetoOceano;
    public GameObject objetoNivel3;

    [Header("Los 3 Textos de Títulos")]
    public GameObject textoPlaya;
    public GameObject textoMar;
    public GameObject textoMarProfundo;

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
        // Activa o desactiva las imágenes según el escenario seleccionado
        if (objetoPlaya != null) objetoPlaya.SetActive(indiceActual == 0);
        if (objetoOceano != null) objetoOceano.SetActive(indiceActual == 1);
        if (objetoNivel3 != null) objetoNivel3.SetActive(indiceActual == 2);

        // Activa o desactiva el texto correspondiente a cada imagen
        if (textoPlaya != null) textoPlaya.SetActive(indiceActual == 0);
        if (textoMar != null) textoMar.SetActive(indiceActual == 1);
        if (textoMarProfundo != null) textoMarProfundo.SetActive(indiceActual == 2);
    }

    public void CargarEscenarioSeleccionado()
    {
        Time.timeScale = 1f;
        if (indiceActual == 0)
        {
            SceneManager.LoadScene("Nivel 1 Playa");
        }
        else if (indiceActual == 1)
        {
            SceneManager.LoadScene("Nivel 2 Mar");
        }
        else if (indiceActual == 2)
        {
            SceneManager.LoadScene("Nivel 3 Mar Profundo");
        }
    }
}