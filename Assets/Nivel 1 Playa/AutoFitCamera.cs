using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AutoFitCamera : MonoBehaviour
{
    // Aquí defines el ancho base de tu diseño (por ejemplo, los 19.2 de tu resolución de referencia)
    public float targetAspectWidth = 16.0f;
    public float targetAspectHeight = 9.0f;
    public float defaultOrthographicSize = 5.0f;

    void Start()
    {
        AdjustCamera();
    }

    void Update()
    {
        // Esto asegura que si el usuario redimensiona la ventana en tiempo real, se ajuste solo
        AdjustCamera();
    }

    void AdjustCamera()
    {
        Camera cam = GetComponent<Camera>();
        float targetAspect = targetAspectWidth / targetAspectHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Calcula la escala para que la pantalla siempre cubra el área sin dejar espacios vacíos
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            cam.orthographicSize = defaultOrthographicSize / scaleHeight;
        }
        else
        {
            cam.orthographicSize = defaultOrthographicSize;
        }
    }
}