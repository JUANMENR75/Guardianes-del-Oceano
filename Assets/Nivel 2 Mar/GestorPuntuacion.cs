using UnityEngine;
using TMPro;

public class GestorPuntuacion : MonoBehaviour
{
    [Header("Referencias de Textos del HUD")]
    public TextMeshProUGUI NumeroMonedas;
    public TextMeshProUGUI NumeroBasura;
    public TextMeshProUGUI NumeroDistancia;

    private int contadorMonedas = 0;
    private int contadorBasura = 0;
    private int distanciaRecorrida = 0; // Ahora es entero para evitar fracciones
    private float tiempoAcumulado = 0f;

    public int Monedas => contadorMonedas;
    public int Basura => contadorBasura;
    public int Distancia => distanciaRecorrida; // Devuelve número entero sólido

    void Update()
    {
        // Controlamos el tiempo para que avance más lento (ej. 1 metro cada 0.8 segundos)
        tiempoAcumulado += Time.deltaTime;

        if (tiempoAcumulado >= 0.8f)
        {
            distanciaRecorrida++;
            tiempoAcumulado = 0f;
        }

        ActualizarHUD();
    }

    void ActualizarHUD()
    {
        if (NumeroMonedas != null)
            NumeroMonedas.text = contadorMonedas.ToString();

        if (NumeroBasura != null)
            NumeroBasura.text = contadorBasura.ToString();

        if (NumeroDistancia != null)
            NumeroDistancia.text = distanciaRecorrida.ToString() + " m";
    }

    public void SumarMoneda(int cantidad = 1)
    {
        contadorMonedas += cantidad;
    }

    public void SumarBasura(int cantidad = 1)
    {
        contadorBasura += cantidad;
    }

    public void SumarDistancia(int cantidad)
    {
        distanciaRecorrida += cantidad;
    }
}