using UnityEngine;
using UnityEngine.UI;

public class ContenedorBasura : MonoBehaviour
{
    [Header("Configuración del Contenedor")]
    public string tipoBasuraAceptada; // "Orgánico", "Inorgánico", "NoReciclable"

    [Header("Nombres de las Barras en el Canvas")]
    public string nombreBarraBasura = "BarraAzul";
    public string nombreBarraPuntos = "BarraNaranja";

    private static Image barraBasuraImg;
    private static Image barraPuntosImg;

    public static int totalBasuraReciclada = 0;
    public static int totalPuntos = 0;

    //  TOTAL piezas para que las barras se llenen mucho más despacio y de forma gradual
    private float piezasMaximasParaLlenar = 50f;

    void Start()
    {
        totalBasuraReciclada = 0;
        totalPuntos = 0;

        GameObject objBasura = GameObject.Find(nombreBarraBasura);
        if (objBasura != null)
        {
            barraBasuraImg = objBasura.GetComponent<Image>();
            barraBasuraImg.fillAmount = 0f;
        }

        GameObject objPuntos = GameObject.Find(nombreBarraPuntos);
        if (objPuntos != null)
        {
            barraPuntosImg = objPuntos.GetComponent<Image>();
            barraPuntosImg.fillAmount = 0f;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.gameObject.name == "PuntoPinza") && Input.GetKey(KeyCode.Space))
        {
            ObjetoBasura[] todasBasuras = FindObjectsByType<ObjetoBasura>(FindObjectsSortMode.None);
            foreach (ObjetoBasura basura in todasBasuras)
            {
                if (basura.estaAtrapada)
                {
                    if (basura.tipoBasura == tipoBasuraAceptada)
                    {
                        totalBasuraReciclada++;
                        totalPuntos += 10; // 10 puntos por pieza

                        basura.DepositarEnContenedor();
                        ActualizarBarrasGradual();

                        ControlPersonaje personaje = FindFirstObjectByType<ControlPersonaje>();
                        if (personaje != null) personaje.DepositarBasuraCorrecta();

                        Debug.Log("¡Reciclado correcto! Total basura: " + totalBasuraReciclada);
                    }
                    else
                    {
                        Debug.Log("¡Contenedor incorrecto!");
                    }
                    break;
                }
            }
        }
    }

    void ActualizarBarrasGradual()
    {
        if (barraBasuraImg != null)
        {
            barraBasuraImg.fillAmount = totalBasuraReciclada / piezasMaximasParaLlenar;
            if (barraBasuraImg.fillAmount > 1f) barraBasuraImg.fillAmount = 1f;
        }

        if (barraPuntosImg != null)
        {
            barraPuntosImg.fillAmount = totalPuntos / (piezasMaximasParaLlenar * 10f);
            if (barraPuntosImg.fillAmount > 1f) barraPuntosImg.fillAmount = 1f;
        }
    }
}