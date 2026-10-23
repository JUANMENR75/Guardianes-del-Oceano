using UnityEngine;

public class GeneradorObjetos : MonoBehaviour
{
    [Header("Configuración del Generador")]
    [Tooltip("Marca esta casilla SI este objeto es exclusivamente para generar BOMBAS.")]
    public bool esGeneradorDeBombas = false;

    [Header("Lista de Prefabs")]
    public GameObject[] prefabs;

    [Header("1. Frecuencia de Aparición (Tiempo)")]
    public float tiempoMinimo = 1.0f;
    public float tiempoMaximo = 4.0f;

    [Header("2. Alturas para Monedas y Basura (Eje Y)")]
    public float alturaMinimaObjetos = -8.3f;
    public float alturaMaximaObjetos = 2.0f;

    [Header("3. Alturas Exclusivas para Bombas (Eje Y)")]
    public float alturaMinimaBombas = -8.3f;
    public float alturaMaximaBombas = 2.0f;

    [Header("4. Ancho de Pantalla (Eje X)")]
    public float xMinimo = 9f;
    public float xMaximo = 16f;

    private float contador;
    private float tiempoSiguienteGeneracion;
    private int ultimoIndice = -1;

    void Start()
    {
        tiempoSiguienteGeneracion = 2.0f;
    }

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tiempoSiguienteGeneracion)
        {
            GenerarObjeto();
            contador = 0;

            float factorDificultad = 1.0f / ControladorDificultad.multiplicadorVelocidad;
            tiempoSiguienteGeneracion = Random.Range(tiempoMinimo * factorDificultad, tiempoMaximo * factorDificultad);
        }
    }

    void GenerarObjeto()
    {
        if (prefabs.Length == 0) return;

        int nuevoIndice;
        do
        {
            nuevoIndice = Random.Range(0, prefabs.Length);
        } while (nuevoIndice == ultimoIndice && prefabs.Length > 1);

        ultimoIndice = nuevoIndice;

        // Selección de altura limpia y separada
        float yAleatorio;
        if (esGeneradorDeBombas)
        {
            yAleatorio = Random.Range(alturaMinimaBombas, alturaMaximaBombas);
        }
        else
        {
            yAleatorio = Random.Range(alturaMinimaObjetos, alturaMaximaObjetos);
        }

        float xAleatorio = Random.Range(xMinimo, xMaximo);
        Vector3 posicionSpawn = new Vector3(xAleatorio, yAleatorio, 0f);

        Instantiate(prefabs[nuevoIndice], posicionSpawn, Quaternion.identity);
    }
}