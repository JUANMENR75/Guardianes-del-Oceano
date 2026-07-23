using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IrAlJuego()
    {
        SceneManager.LoadScene("Escena 1");
    }

    public void IrAHerramientas()
    {
        SceneManager.LoadScene("Escena 2");
    }

    // Agrega esta nueva función para los escenarios
    public void IrAEscenarios()
    {
        SceneManager.LoadScene("Escena 3"); // Asegúrate de que tu escena se llame exactamente así
    }

    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}