using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarraProgresoUI : MonoBehaviour
{
    private Image barraImagen;

    [Header("Configuración del Tiempo")]
    private float tiempoTotal = 90f;
    private static float tiempoActual;

    [Header("Panel de Game Over")]
    public GameObject panelGameOverAsignado;

    void Start()
    {
        barraImagen = GetComponent<Image>();

        if (gameObject.name.Contains("Tiempo") || gameObject.name.Contains("Verde"))
        {
            tiempoActual = tiempoTotal;

            if (panelGameOverAsignado != null)
            {
                panelGameOverAsignado.SetActive(false);
            }
        }

        if (barraImagen != null)
        {
            barraImagen.type = Image.Type.Filled;
            barraImagen.fillMethod = Image.FillMethod.Horizontal;
            barraImagen.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
    }

    void Update()
    {
        if (barraImagen == null) return;

        // 1. Control del tiempo
        if (gameObject.name.Contains("Tiempo") || gameObject.name.Contains("Verde"))
        {
            ControladorTiempo ctrlTiempo = Object.FindAnyObjectByType<ControladorTiempo>();
            float tiempoMax = ctrlTiempo != null ? ctrlTiempo.tiempoMaximo : 90f;

            if (tiempoActual > 0f)
            {
                tiempoActual -= Time.deltaTime;
                barraImagen.fillAmount = Mathf.Clamp01(tiempoActual / tiempoMax);
            }
            return;
        }

        // Obtener la meta dinámica desde el ControladorTiempo de la escena
        ControladorTiempo controladorTiempo = Object.FindAnyObjectByType<ControladorTiempo>();
        float metaDinamica = 35f; // Valor por defecto
        if (controladorTiempo != null)
        {
            metaDinamica = (float)controladorTiempo.metaBasuraVictoria;
        }

        // 2. Barra de basura (se adapta automáticamente a la meta elegida)
        if (gameObject.name == "BarraAzulBasura")
        {
            barraImagen.fillAmount = Mathf.Clamp01((float)ContenedorBasura.totalBasuraReciclada / metaDinamica);
        }
        // 3. Barra de puntos (cada pieza vale 10 puntos)
        else if (gameObject.name == "BarraNaranjaPuntos")
        {
            barraImagen.fillAmount = Mathf.Clamp01((float)ContenedorBasura.totalPuntos / (metaDinamica * 10f));
        }
    }
}