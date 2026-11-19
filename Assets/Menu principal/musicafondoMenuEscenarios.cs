using UnityEngine;
using UnityEngine.SceneManagement;

public class musicafondoMenuEscenarios : MonoBehaviour
{
    private static musicafondoMenuEscenarios instancia;

    void Awake()
    {
        // Evita que se duplique la música si regresas de Escenarios al MenuPrincipal
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += AlCambiarEscena;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= AlCambiarEscena;
    }

    void AlCambiarEscena(Scene escena, LoadSceneMode modo)
    {
        // Solo permanece viva en MenuPrincipal y Escenarios; en cualquier nivel se detiene y destruye
        if (escena.name != "MenuPrincipal" && escena.name != "Escenarios")
        {
            instancia = null;
            Destroy(gameObject);
        }
    }
}