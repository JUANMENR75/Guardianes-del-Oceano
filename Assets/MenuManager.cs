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

    
    public void IrAEscenarios()
    {
        SceneManager.LoadScene("Escenarios"); 
    }

    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}