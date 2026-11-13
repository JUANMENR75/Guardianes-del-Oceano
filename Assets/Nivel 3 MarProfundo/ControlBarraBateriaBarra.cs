using UnityEngine;
using UnityEngine.UI;

public class ControlBarraBateriaBarra : MonoBehaviour
{
    [Header("Referencia al Relleno")]
    public Image imagenRelleno; // Arrastra aquí la imagen verde del relleno

    [Header("Referencia al Submarino")]
    private MovimientoSubmarino submarino; // Busca automáticamente el script de tu submarino

    [Header("Rangos de los 4 Colores")]
    public Color colorVerde = new Color(0.2f, 0.8f, 0.2f);     // 75% - 100%
    public Color colorAmarillo = new Color(1f, 0.9f, 0.1f);    // 50% - 74%
    public Color colorNaranja = new Color(1f, 0.5f, 0f);       // 25% - 49%
    public Color colorRojo = new Color(0.9f, 0.2f, 0.2f);      // Menos de 25%

    void Start()
    {
        // Buscamos automáticamente el script del submarino en la escena
        submarino = FindAnyObjectByType<MovimientoSubmarino>();
    }

    void Update()
    {
        if (submarino == null) return;

        // Calculamos el porcentaje en tiempo real basándonos en la batería actual del submarino
        float bateriaActual = submarino.bateriaActual;
        float bateriaMax = 100f; // Asumiendo que el máximo es 100

        float porcentaje = bateriaActual / bateriaMax;

        if (imagenRelleno != null)
        {
            // La barra baja automáticamente solita
            imagenRelleno.fillAmount = porcentaje;

            // Cambia de color automáticamente según los 4 rangos
            if (porcentaje > 0.75f)
            {
                imagenRelleno.color = colorVerde;      // Verde (Óptimo)
            }
            else if (porcentaje > 0.50f)
            {
                imagenRelleno.color = colorAmarillo;   // Amarillo (Precaución)
            }
            else if (porcentaje > 0.25f)
            {
                imagenRelleno.color = colorNaranja;    // Naranja (Advertencia)
            }
            else
            {
                imagenRelleno.color = colorRojo;       // Rojo (Peligro crítico)
            }
        }
    }
}