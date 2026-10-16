using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarraProgresoUI : MonoBehaviour
{
    private Image barraImagen;
    private float maxPiezas = 30f; // TOTAL DE PIEZAS DE BASURA QUE SE POUDEN PONER EN LAS BARRA

    [Header("Configuración del Tiempo")]
    private float tiempoTotal = 20f;  // TOTAL DE TIEMPO QUE SE POUDEN PONER EN LAS BARRA
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
            if (tiempoActual > 0f)
            {
                tiempoActual -= Time.deltaTime;
                barraImagen.fillAmount = Mathf.Clamp01(tiempoActual / tiempoTotal);
            }
            else
            {
                if (panelGameOverAsignado != null && !panelGameOverAsignado.activeSelf)
                {
                    panelGameOverAsignado.SetActive(true);

                    // Asignar los totales exactos usando los nombres de tu jerarquía
                    AsignarTotalesExactos();
                }
            }
            return;
        }

        // 2. Barra de basura
        if (gameObject.name == "BarraAzulBasura")
        {
            barraImagen.fillAmount = Mathf.Clamp01((float)ContenedorBasura.totalBasuraReciclada / maxPiezas);
        }
        // 3. Barra de puntos (cada pieza vale 10 puntos)
        else if (gameObject.name == "BarraNaranjaPuntos")
        {
            barraImagen.fillAmount = Mathf.Clamp01((float)ContenedorBasura.totalPuntos / (maxPiezas * 10f));
        }
    }

    void AsignarTotalesExactos()
    {
        if (panelGameOverAsignado == null) return;

        string valBasura = ContenedorBasura.totalBasuraReciclada.ToString();
        string valPuntos = ContenedorBasura.totalPuntos.ToString();

        // Buscar exactamente por los nombres de tu jerarquía: "Text Basura" y "TextPuntos"
        Transform tBasura = panelGameOverAsignado.transform.Find("Text Basura");
        Transform tPuntos = panelGameOverAsignado.transform.Find("TextPuntos");

        if (tBasura != null)
        {
            AsignarTextoUniversal(tBasura, valBasura);
        }

        if (tPuntos != null)
        {
            AsignarTextoUniversal(tPuntos, valPuntos);
        }
    }

    void AsignarTextoUniversal(Transform objetoHijo, string textoNuevo)
    {
        Text txtStandard = objetoHijo.GetComponent<Text>();
        if (txtStandard != null) { txtStandard.text = textoNuevo; return; }

        TextMeshProUGUI txtTMP = objetoHijo.GetComponent<TextMeshProUGUI>();
        if (txtTMP != null) { txtTMP.text = textoNuevo; return; }

        Text txtHijo = objetoHijo.GetComponentInChildren<Text>();
        if (txtHijo != null) { txtHijo.text = textoNuevo; return; }

        TextMeshProUGUI txtTMPHijo = objetoHijo.GetComponentInChildren<TextMeshProUGUI>();
        if (txtTMPHijo != null) { txtTMPHijo.text = textoNuevo; return; }
    }
}