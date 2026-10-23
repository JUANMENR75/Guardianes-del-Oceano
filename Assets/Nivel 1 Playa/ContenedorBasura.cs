using UnityEngine;
using UnityEngine.UI;

public class ContenedorBasura : MonoBehaviour
{
    [Header("Configuración del Contenedor")]
    public string tipoBasuraAceptada;

    public static int totalBasuraReciclada = 0;
    public static int totalPuntos = 0;

    private static Image barraBasuraGlobal;
    private static Image barraPuntosGlobal;

    void Start()
    {
        totalBasuraReciclada = 0;
        totalPuntos = 0;

        GameObject objBasura = GameObject.Find("BarraAzulBasura");
        if (objBasura != null) barraBasuraGlobal = objBasura.GetComponent<Image>();

        GameObject objPuntos = GameObject.Find("BarraNaranjaPuntos");
        if (objPuntos != null) barraPuntosGlobal = objPuntos.GetComponent<Image>();

        ActualizarBarraVisualDirecta();
    }

    public void RecibirBasura(ObjetoBasura basura)
    {
        if (basura == null) return;

        string basuraLimpia = LimpiarTexto(basura.tipoBasura);
        string contenedorLimpio = LimpiarTexto(tipoBasuraAceptada);

        if (basuraLimpia == contenedorLimpio)
        {
            totalBasuraReciclada++;
            totalPuntos += 10;

            basura.DepositerEnContenedorLocal();
            ActualizarBarraVisualDirecta();

            Debug.Log($"¡Depósito correcto! Basura: {totalBasuraReciclada} | Puntos: {totalPuntos}");
        }
        else
        {
            Debug.Log("¡Contenedor incorrecto!");
        }
    }

    public static void ActualizarBarraVisualDirecta()
    {
        ControladorTiempo controladorTiempo = Object.FindAnyObjectByType<ControladorTiempo>();
        float metaDinamica = 35f;

        if (controladorTiempo != null)
        {
            metaDinamica = (float)controladorTiempo.metaBasuraVictoria;
        }

        if (barraBasuraGlobal != null)
        {
            barraBasuraGlobal.fillAmount = Mathf.Clamp01((float)totalBasuraReciclada / metaDinamica);
        }

        if (barraPuntosGlobal != null)
        {
            barraPuntosGlobal.fillAmount = Mathf.Clamp01((float)totalPuntos / (metaDinamica * 10f));
        }
    }

    string LimpiarTexto(string texto)
    {
        if (string.IsNullOrEmpty(texto)) return "";
        texto = texto.Trim().ToLower();
        texto = texto.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        return texto;
    }
}