using UnityEngine;

public class GeneradorObjetos : MonoBehaviour
{
    public GameObject[] prefabs;

    [Header("Configuración de Tiempo")]
    public float tiempoMinimo = 1.0f; // Más rápido
    public float tiempoMaximo = 2.0f; // Más rápido

    private float contador;
    private float tiempoSiguienteGeneracion;
    private int ultimoIndice = -1; // Para evitar repetir el mismo objeto siempre

    void Start()
    {
        tiempoSiguienteGeneracion = 1.0f;
    }

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tiempoSiguienteGeneracion)
        {
            GenerarObjeto();
            contador = 0;

            // Calculamos el próximo tiempo
            float factorDificultad = 1.0f / ControladorDificultad.multiplicadorVelocidad;
            tiempoSiguienteGeneracion = Random.Range(tiempoMinimo * factorDificultad, tiempoMaximo * factorDificultad);
        }
    }

    void GenerarObjeto()
    {
        // 1. Elegir un índice que no sea el mismo que el anterior
        int nuevoIndice;
        do
        {
            nuevoIndice = Random.Range(0, prefabs.Length);
        } while (nuevoIndice == ultimoIndice && prefabs.Length > 1);

        ultimoIndice = nuevoIndice;

        // 2. Generar posición
        float yAleatorio = Random.Range(-7.5f, -0.5f);
        Vector3 posicionSpawn = new Vector3(14f, yAleatorio, 0f);

        // 3. Crear el objeto
        Instantiate(prefabs[nuevoIndice], posicionSpawn, Quaternion.identity);
    }
}