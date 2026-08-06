using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorPausa : MonoBehaviour
{
    public GameObject panelPausa;

    void Start()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    void Update()
    {
        // Detecta la tecla Escape en cualquier momento para abrir o cerrar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelPausa != null && panelPausa.activeSelf)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }
        Time.timeScale = 0f; // Congela el juego
        AudioListener.pause = true; // Pausa toda la música y sonidos
    }

    public void Reanudar()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
        Time.timeScale = 1f; // Restaura el tiempo normal
        AudioListener.pause = false; // Reactiva la música y sonidos
    }

    public void ReiniciarJuego()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal(string nombreMenu)
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreMenu);
    }
}