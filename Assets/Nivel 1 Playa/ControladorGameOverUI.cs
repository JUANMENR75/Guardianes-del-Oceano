using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGameOverUI : MonoBehaviour
{
    [Header("Panel de Victoria")]
    public GameObject panelVictoria;

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;

        ContenedorBasura.totalBasuraReciclada = 0;
        ContenedorBasura.totalPuntos = 0;
        ObjetoBasura.pinzaOcupada = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ActivarVictoria()
    {
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }

        // Destruye la basura por componente
        ObjetoBasura[] basurasPorScript = Object.FindObjectsByType<ObjetoBasura>(FindObjectsSortMode.None);
        foreach (ObjetoBasura b in basurasPorScript)
        {
            if (b != null) Destroy(b.gameObject);
        }

        // Destruye objetos por etiquetas de clasificación
        string[] tagsBasura = { "Inorganico", "NoReciclable", "Organico", "Basura" };
        foreach (string t in tagsBasura)
        {
            GameObject[] objetosConTag = GameObject.FindGameObjectsWithTag(t);
            foreach (GameObject obj in objetosConTag)
            {
                if (obj != null) Destroy(obj);
            }
        }

        // Destruye cualquier residuo restante por nombre en la escena
        GameObject[] todosLosObjetos = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in todosLosObjetos)
        {
            if (obj != null)
            {
                string n = obj.name;
                if (n.Contains("Botella") || n.Contains("Bolsa") || n.Contains("Caja") ||
                    n.Contains("Cigarro") || n.Contains("Envoltura") || n.Contains("Lata") ||
                    n.Contains("Manzana") || n.Contains("Piña") || n.Contains("Platano") ||
                    n.Contains("Sandia") || n.Contains("Vaso"))
                {
                    Destroy(obj);
                }
            }
        }

        // Apaga el generador de basura
        GameObject generador = GameObject.Find("GeneradorBasura");
        if (generador != null)
        {
            generador.SetActive(false);
        }
    }
}