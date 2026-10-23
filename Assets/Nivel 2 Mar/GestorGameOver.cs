using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorGameOver : MonoBehaviour
{
    [Header("Panel y Botones de Game Over")]
    public GameObject panelGameOver;
    public GameObject botonJugarDeNuevo;
    public GameObject botonMenuPrincipal;

    public void ActivarGameOver()
    {
        Debug.Log(">>> ¡Ejecutando ActivarGameOver en el Gestor! <<<");

        // Búsqueda de emergencia por si el panel no está asignado
        if (panelGameOver == null)
        {
            panelGameOver = GameObject.Find("PanelGameOver");
        }

        // Activamos visualmente el panel de Game Over en la pantalla
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            Debug.Log("¡Panel GameOver activado con éxito en pantalla!");
        }
        else
        {
            Debug.LogError("¡ERROR CRÍTICO: No se encuentra el PanelGameOver en la escena!");
        }

        // Forzamos a que los botones también se enciendan al perder
        if (botonJugarDeNuevo != null) botonJugarDeNuevo.SetActive(true);
        if (botonMenuPrincipal != null) botonMenuPrincipal.SetActive(true);

        // Pausar el juego al perder
        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Restaurar el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Restaurar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("MenuPrincipal"); // Cambia por el nombre exacto de tu escena de menú si es diferente
    }
}