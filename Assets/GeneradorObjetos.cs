using UnityEngine;

public class GeneradorObjetos : MonoBehaviour
{
    public GameObject[] prefabs; // Arrastra aquí la Moneda y la Basura
    public float tiempoEntreGeneracion = 2.0f;
    private float contador;

    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= tiempoEntreGeneracion)
        {
            // Genera en el lado derecho (X=10) y en una altura aleatoria
            float yAleatorio = Random.Range(-3.5f, -0.8f);
            Vector3 posicionSpawn = new Vector3(10f, yAleatorio, 0);

            // Elige aleatoriamente entre los prefabs asignados
            int indice = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[indice], posicionSpawn, Quaternion.identity);

            contador = 0;
        }
    }
}