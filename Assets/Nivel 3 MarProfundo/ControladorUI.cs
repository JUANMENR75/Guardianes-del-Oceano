using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorUI : MonoBehaviour
{
    public void ReiniciarNivel()
    {
        Debug.Log("¡Se hizo clic en Reiniciar!");
        Time.timeScale = 1f; // Restaura el tiempo PRIMERO para limpiar la pausa
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        Debug.Log("¡Se hizo clic en Menú Principal!");
        Time.timeScale = 1f; // Restaura el tiempo PRIMERO
        SceneManager.LoadScene("MenuPrincipal");
    }
}