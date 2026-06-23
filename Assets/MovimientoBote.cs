using UnityEngine;

public class MovimientoBote : MonoBehaviour
{
    public float amplitudVertical = 0.15f;
    public float velocidadVertical = 1.5f;

    public float amplitudRotacion = 5f;
    public float velocidadRotacion = 1.2f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float movimientoY = Mathf.Sin(Time.time * velocidadVertical) * amplitudVertical;
        transform.position = posicionInicial + new Vector3(0, movimientoY, 0);

        float rotacionZ = Mathf.Sin(Time.time * velocidadRotacion) * amplitudRotacion;
        transform.rotation = Quaternion.Euler(0, 0, rotacionZ);
    }
}