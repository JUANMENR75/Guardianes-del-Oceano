using UnityEngine;

public class GeneradorObjetos : MonoBehaviour
{
    public GameObject[] prefabs;

    // Estos valores son mucho más altos para obligar a que haya espacio
    public float tiempoMinimo = 3.0f;
    public float tiempoMaximo = 10.0f;

    private float contador;
    private float tiempoSiguienteGeneracion;

    void Start()
    {
        tiempoSiguienteGeneracion = 2.0f;
    }

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tiempoSiguienteGeneracion)
        {
            // Rango de altura más restringido (Y) 
            // -2.0f es la altura del barco, -0.5f es debajo de los marcadores
            float yAleatorio = Random.Range(-7.5f, -0.5f);

            // X=14 los hace aparecer más lejos fuera de pantalla
            Vector3 posicionSpawn = new Vector3(14f, yAleatorio, 0f);

            int indice = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[indice], posicionSpawn, Quaternion.identity);

            contador = 0;
            // Nueva espera larga
            tiempoSiguienteGeneracion = Random.Range(tiempoMinimo, tiempoMaximo);
        }
    }
}