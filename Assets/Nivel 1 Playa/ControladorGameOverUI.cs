using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGameOverUI : MonoBehaviour
{
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Restaura el tiempo normal

        // Reinicia las variables globales
        ContenedorBasura.totalBasuraReciclada = 0;
        ContenedorBasura.totalPuntos = 0;
        ObjetoBasura.pinzaOcupada = false;

        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal"); // Cambia por el nombre exacto de tu escena de menú si es diferente
    }
}