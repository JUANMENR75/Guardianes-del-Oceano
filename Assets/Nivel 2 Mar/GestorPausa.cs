using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestorPausa : MonoBehaviour
{
    public GameObject panelPausa;
    public TextMeshProUGUI textoContador; // Tu TextContador123

    void Start()
    {
        // 1. Asegurar que el menú de pausa esté apagado
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }

        // 2. Apagar el texto del contador apenas inicia la escena para que no se quede estático en "3"
        if (textoContador != null)
        {
            textoContador.gameObject.SetActive(false);
        }

        // 3. Lanzar la cuenta regresiva automáticamente al entrar al nivel o reiniciar
        StartCoroutine(RutinaCuentaRegresiva());
    }

    void Update()
    {
        // Detecta la tecla Escape para pausar o reanudar
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
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Reanudar()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }

        // Cuenta regresiva al reanudar desde pausa
        StartCoroutine(RutinaCuentaRegresiva());
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

    // Corrutina que hace la cuenta regresiva oculta al inicio y la muestra paso a paso
    IEnumerator RutinaCuentaRegresiva()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (textoContador != null)
        {
            textoContador.gameObject.SetActive(true);

            textoContador.fontSize = 130; // Tamaño grande para los números
            textoContador.text = "3";
            yield return new WaitForSecondsRealtime(1f);

            textoContador.text = "2";
            yield return new WaitForSecondsRealtime(1f);

            textoContador.text = "1";
            yield return new WaitForSecondsRealtime(1f);

            textoContador.fontSize = 80; // Tamaño más pequeño solo para el ¡YA!
            textoContador.text = "¡YA!";
            yield return new WaitForSecondsRealtime(0.7f);

            textoContador.gameObject.SetActive(false);
        }

        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}