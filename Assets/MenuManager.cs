using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Variable para saber si venimos a jugar o solo a ver la muestra
    public static bool modoJuegoActivo = false;

    // 1. Botón Jugar: SÍ activa el modo de juego y va a escenarios
    public void IrAEscenarios()
    {
        modoJuegoActivo = true; // Activa los botones de nivel
        SceneManager.LoadScene("Escenarios");
    }

    // 2. Botón Herramientas
    public void IrAHerramientas()
    {
        SceneManager.LoadScene("Herramientas");
    }

    // 3. Botón Escenarios (Muestra): Entra a escenarios pero APAGA el modo de juego
    public void IrAMuestraEscenarios()
    {
        modoJuegoActivo = false; // Desactiva los botones de nivel (solo muestra)
        SceneManager.LoadScene("Escenarios");
    }

    // 4. Funciones para los niveles reales
    public void IrANivelPlaya()
    {
        if (modoJuegoActivo) SceneManager.LoadScene("Nivel 1 Playa");
    }

    public void IrANivelMar()
    {
        if (modoJuegoActivo) SceneManager.LoadScene("Nivel 2 Mar");
    }

    // 5. Regresar al Menú Principal
    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}