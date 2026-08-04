using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IrAlJuego()
    {
        SceneManager.LoadScene("Nivel 2 Mar");
    }

    public void IrAHerramientas()
    {
        SceneManager.LoadScene("Herramientas");
    }

    // Agrega esta nueva función para los escenarios
    public void IrAEscenarios()
    {
        SceneManager.LoadScene("Escenarios"); // Asegúrate de que tu escena se llame exactamente así
    }

    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}