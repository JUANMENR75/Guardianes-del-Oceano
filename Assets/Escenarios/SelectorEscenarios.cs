using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectorEscenarios : MonoBehaviour
{
    [Header("Los 3 Escenarios Disponibles")]
    public GameObject objetoPlaya;
    public GameObject objetoOceano;
    public GameObject objetoNivel3;

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
        // Activa o desactiva las imágenes según el escenario seleccionado
        if (objetoPlaya != null) objetoPlaya.SetActive(indiceActual == 0);
        if (objetoOceano != null) objetoOceano.SetActive(indiceActual == 1);
        if (objetoNivel3 != null) objetoNivel3.SetActive(indiceActual == 2);

        // Actualiza el texto en pantalla según el índice
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
            else if (indiceActual == 2)
            {
                textoTitulo.text = "NIVEL 3";
            }
        }
    }

    // Método opcional por si necesitas que el botón de Jugar cargue la escena según el escenario activo
    public void CargarEscenarioSeleccionado()
    {
        Time.timeScale = 1f;
        if (indiceActual == 0)
        {
            SceneManager.LoadScene("Nivel 1 Playa"); // Cambia por el nombre exacto de tu escena de playa
        }
        else if (indiceActual == 1)
        {
            SceneManager.LoadScene("Nivel 2 Mar"); // Cambia por el nombre exacto de tu escena de mar
        }
        else if (indiceActual == 2)
        {
            SceneManager.LoadScene("Nivel 3 MarProfundo"); // El nombre de tu escena actual
        }
    }
}