using UnityEngine;
using TMPro;

public class GestorPuntuacion : MonoBehaviour
{
    public TextMeshProUGUI numeroMonedas;
    public TextMeshProUGUI numeroBasura;
    public TextMeshProUGUI numeroDistancia;

    // Estas variables ahora son privadas para seguridad, pero las propiedades públicas 
    // de abajo permiten que otros scripts las lean.
    private int contMonedas = 0;
    private int contBasura = 0;
    private float distancia = 0;

    // Propiedades públicas para que GestorGameOver pueda obtener los valores
    public int Monedas => contMonedas;
    public int Basura => contBasura;
    public int Distancia => Mathf.RoundToInt(distancia);

    void Start()
    {
        if (numeroMonedas != null) numeroMonedas.text = "0";
        if (numeroBasura != null) numeroBasura.text = "0";
        if (numeroDistancia != null) numeroDistancia.text = "0";
    }

    public void SumarMoneda()
    {
        contMonedas++;
        if (numeroMonedas != null) numeroMonedas.text = contMonedas.ToString();
    }

    public void SumarBasura()
    {
        contBasura++;
        if (numeroBasura != null) numeroBasura.text = contBasura.ToString();
    }

    void Update()
    {
        distancia += Time.deltaTime;
        if (numeroDistancia != null)
            numeroDistancia.text = Distancia.ToString();
    }
}