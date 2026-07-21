using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IrAlJuego()
    {
        SceneManager.LoadScene("Escena");
    }

    public void IrAHerramientas()
    {
        SceneManager.LoadScene("Escena 2");
    }

    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal"); // Cambia "MenuPrincipal" por el nombre exacto de tu escena de menú inicial
    }
}