using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GestorGameOver : MonoBehaviour
{
    public GameObject panelGameOver; // Arrastra aquí tu PanelGameOver
    public TextMeshProUGUI textoMonedas; // Arrastra tu texto de monedas
    public TextMeshProUGUI textoBasura;  // Arrastra tu texto de basura
    public TextMeshProUGUI textoDistancia; // Arrastra tu texto de distancia

    public GestorPuntuacion gestorPuntuacion; // Arrastra tu objeto que tiene el script GestorPuntuacion

    public void ActivarGameOver()
    {
        // Esto detiene el tiempo y todo movimiento basado en Time.deltaTime
        Time.timeScale = 0;

        // Esto hace que el panel sea visible
        panelGameOver.SetActive(true);

        // Actualizamos los textos
        textoMonedas.text = "Monedas: " + gestorPuntuacion.Monedas;
        textoBasura.text = "Basura: " + gestorPuntuacion.Basura;
        textoDistancia.text = "Distancia: " + gestorPuntuacion.Distancia + " m";
    }

    public void ReiniciarJuego()
    {
        Debug.Log("Reiniciando juego...");
        Time.timeScale = 1; // ¡Esto es clave! Devuelve el tiempo a la normalidad

        // Cargamos la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1; // Reanudamos el tiempo antes de salir
        SceneManager.LoadScene("MenuPrincipal"); // Cambia "MenuPrincipal" por el nombre exacto de tu escena
    }
}