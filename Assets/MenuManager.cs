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
}