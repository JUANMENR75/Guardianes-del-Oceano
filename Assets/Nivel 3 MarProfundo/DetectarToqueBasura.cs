using UnityEngine;

public class DetectarToqueBasura : MonoBehaviour
{
    [HideInInspector] public GestorBasura gestor;
    [HideInInspector] public int puntosOtorgados;

    void OnTriggerEnter2D(Collider2D otro)
    {
        MovimientoSubmarino sub = otro.GetComponent<MovimientoSubmarino>();
        if (sub != null && gestor != null)
        {
            gestor.RecogerBasura(gameObject, puntosOtorgados);
        }
    }
}