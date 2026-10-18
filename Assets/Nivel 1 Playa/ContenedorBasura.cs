using UnityEngine;
using UnityEngine.UI;

public class ContenedorBasura : MonoBehaviour
{
    [Header("Configuración del Contenedor")]
    public string tipoBasuraAceptada; // "Orgánico", "Inorgánico", "NoReciclable"

    // Contadores globales compartidos para el juego y el Game Over
    public static int totalBasuraReciclada = 0;
    public static int totalPuntos = 0;

    // Referencias globales estáticas para las barras actualizadas
    private static Image barraBasuraGlobal;
    private static Image barraPuntosGlobal;

    // 10 piezas máximo para que la barra se llene al 100% y se note rápido con cada pieza
    private static float piezasMaximasParaLlenar = 35f;

    void Start()
    {
        totalBasuraReciclada = 0;
        totalPuntos = 0;

        // Búsqueda automática con los NUEVOS nombres exactos que pusiste en el Canvas
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
            // Sumamos los totales
            totalBasuraReciclada++;
            totalPuntos += 10; // 10 puntos por cada pieza correcta

            basura.DepositerEnContenedorLocal(); // Destruye la basura recolectada
            ActualizarBarraVisualDirecta();       // Actualiza las barras visuales al instante en tiempo real

            Debug.Log($"¡Depósito correcto! Basura: {totalBasuraReciclada} | Puntos: {totalPuntos}");
        }
        else
        {
            Debug.Log("¡Contenedor incorrecto!");
        }
    }

    public static void ActualizarBarraVisualDirecta()
    {
        // Actualiza la barra azul de basura en tiempo real
        if (barraBasuraGlobal != null)
        {
            barraBasuraGlobal.fillAmount = Mathf.Clamp01((float)totalBasuraReciclada / piezasMaximasParaLlenar);
        }

        // Actualiza la barra naranja de puntos en tiempo real (10 piezas * 10 puntos = 100 máx)
        if (barraPuntosGlobal != null)
        {
            barraPuntosGlobal.fillAmount = Mathf.Clamp01((float)totalPuntos / (piezasMaximasParaLlenar * 10f));
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