using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectorHerramientas : MonoBehaviour
{
    [System.Serializable]
    public struct DatosHerramienta
    {
        public string nombreHerramienta;
        public Sprite iconoHerramienta;
        [TextArea] public string descripcionHerramienta;
    }

    [Header("Configuración Exclusiva de Herramientas")]
    public DatosHerramienta[] misHerramientas; // Tus 4 herramientas configuradas en el Inspector

    [Header("UI Tarjeta Izquierda")]
    public Image panelImagenIzq;
    public TextMeshProUGUI textoNombreIzq;
    public TextMeshProUGUI textoDescIzq;

    [Header("UI Tarjeta Derecha")]
    public Image panelImagenDer;
    public TextMeshProUGUI textoNombreDer;
    public TextMeshProUGUI textoDescDer;

    private int indiceSeleccionado = 0;

    void Start()
    {
        ActualizarVistaHerramientas();
    }

    // Método único para la flecha izquierda (Rotar de 2 en 2)
    public void AnteriorHerramienta()
    {
        indiceSeleccionado += 2;

        // Si nos pasamos del total de herramientas, regresamos al inicio de forma segura
        if (indiceSeleccionado >= misHerramientas.Length)
        {
            indiceSeleccionado = 0;
        }

        ActualizarVistaHerramientas();
    }

    void ActualizarVistaHerramientas()
    {
        if (misHerramientas.Length == 0) return;

        int idxIzq = indiceSeleccionado;
        int idxDer = (indiceSeleccionado + 1 < misHerramientas.Length) ? indiceSeleccionado + 1 : 0;

        // Asignar datos visuales a la tarjeta izquierda
        panelImagenIzq.sprite = misHerramientas[idxIzq].iconoHerramienta;
        textoNombreIzq.text = misHerramientas[idxIzq].nombreHerramienta;
        textoDescIzq.text = misHerramientas[idxIzq].descripcionHerramienta;

        // Asignar datos visuales a la tarjeta derecha
        panelImagenDer.sprite = misHerramientas[idxDer].iconoHerramienta;
        textoNombreDer.text = misHerramientas[idxDer].nombreHerramienta;
        textoDescDer.text = misHerramientas[idxDer].descripcionHerramienta;

        // --- TAMAÑO EXCLUSIVO PARA LA DESCRIPCIÓN DEL TURBO ---
        textoDescIzq.enableAutoSizing = false;
        textoDescDer.enableAutoSizing = false;

        // Izquierda
        if (textoDescIzq.text == "Aumenta la velocidad por unos segundos")
            textoDescIzq.fontSize = 13; // Tamaño más chico solo para esta descripción
        else
            textoDescIzq.fontSize = 16; // Tamaño estándar para las demás

        // Derecha
        if (textoDescDer.text == "Aumenta la velocidad por unos segundos")
            textoDescDer.fontSize = 13; // Tamaño más chico solo para esta descripción
        else
            textoDescDer.fontSize = 16; // Tamaño estándar para las demás
    }
}