using UnityEngine;
using TMPro;

public class GestorPuntuacion : MonoBehaviour
{
    public TextMeshProUGUI numeroMonedas;
    public TextMeshProUGUI numeroBasura;
    public TextMeshProUGUI numeroDistancia;

    private int contMonedas = 0;
    private int contBasura = 0;
    private float distancia = 0;

    void Start()
    {
        // Aseguramos que los contadores inicien en 0 visualmente
        if (numeroMonedas != null) numeroMonedas.text = "0";
        if (numeroBasura != null) numeroBasura.text = "0";
        if (numeroDistancia != null) numeroDistancia.text = "0";
    }

    public void SumarMoneda()
    {
        contMonedas++;
        if (numeroMonedas != null)
            numeroMonedas.text = contMonedas.ToString();
    }

    public void SumarBasura()
    {
        contBasura++;
        if (numeroBasura != null)
            numeroBasura.text = contBasura.ToString();
    }

    void Update()
    {
        distancia += Time.deltaTime;
        if (numeroDistancia != null)
            numeroDistancia.text = Mathf.RoundToInt(distancia).ToString();
    }
}