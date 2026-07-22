using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonAnimadoUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Configuración de Escala")]
    [SerializeField] private Vector3 escalaHover = new Vector3(1.08f, 1.08f, 1.0f);
    [SerializeField] private Vector3 escalaClick = new Vector3(0.92f, 0.92f, 1.0f);

    [Header("Velocidad de Animación")]
    [SerializeField] private float velocidadAnimacion = 15f;

    private Vector3 escalaOriginal;
    private Vector3 escalaObjetivo;
    private Coroutine rutinaAnimacion;

    private void Awake()
    {
        // Guarda el tamaño original del cuadro
        escalaOriginal = transform.localScale;
        escalaObjetivo = escalaOriginal;
    }

    private void OnDisable()
    {
        // Restaura el tamaño si el cuadro se desactiva
        transform.localScale = escalaOriginal;
        escalaObjetivo = escalaOriginal;
    }

    // Se ejecuta al pasar el cursor sobre el cuadro
    public void OnPointerEnter(PointerEventData eventData)
    {
        CambiarEscala(Vector3.Scale(escalaOriginal, escalaHover));
    }

    // Se ejecuta al quitar el cursor del cuadro
    public void OnPointerExit(PointerEventData eventData)
    {
        CambiarEscala(escalaOriginal);
    }

    // Se ejecuta al presionar el clic
    public void OnPointerDown(PointerEventData eventData)
    {
        CambiarEscala(Vector3.Scale(escalaOriginal, escalaClick));
    }

    // Se ejecuta al soltar el clic
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.hovered.Contains(gameObject))
        {
            CambiarEscala(Vector3.Scale(escalaOriginal, escalaHover));
        }
        else
        {
            CambiarEscala(escalaOriginal);
        }
    }

    private void CambiarEscala(Vector3 nuevaEscala)
    {
        escalaObjetivo = nuevaEscala;

        if (rutinaAnimacion != null)
        {
            StopCoroutine(rutinaAnimacion);
        }

        rutinaAnimacion = StartCoroutine(AnimarEscala());
    }

    private IEnumerator AnimarEscala()
    {
        while (Vector3.Distance(transform.localScale, escalaObjetivo) > 0.001f)
        {
            // Usa unscaledDeltaTime para que la animación funcione incluso si el juego está en pausa
            transform.localScale = Vector3.Lerp(transform.localScale, escalaObjetivo, Time.unscaledDeltaTime * velocidadAnimacion);
            yield return null;
        }

        transform.localScale = escalaObjetivo;
    }
}