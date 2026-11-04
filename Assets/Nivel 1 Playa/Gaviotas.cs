using UnityEngine;

public class Gaviotas : MonoBehaviour
{
    [Header("Configuración de Vuelo")]
    [Tooltip("Velocidad de movimiento en píxeles por segundo (ej. 300 o 400).")]
    public float velocidad = 300f;

    [Tooltip("Margen extra en píxeles para que la gaviota salga por completo de la pantalla antes de reaparecer.")]
    public float margenSalida = 100f;

    private RectTransform rectTransform;
    private Canvas canvasPadre;
    private AudioSource audioSourceGaviota;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasPadre = GetComponentInParent<Canvas>();

        // Configurar y reproducir el sonido ambiental en bucle para esta gaviota
        audioSourceGaviota = GetComponent<AudioSource>();
        if (audioSourceGaviota == null)
        {
            audioSourceGaviota = gameObject.AddComponent<AudioSource>();
        }

        if (ControladorSonidoPlaya.Instance != null && ControladorSonidoPlaya.Instance.sonidoGaviota != null)
        {
            audioSourceGaviota.clip = ControladorSonidoPlaya.Instance.sonidoGaviota;
            audioSourceGaviota.loop = true; // Sonido constante ambiental
            audioSourceGaviota.Play();
        }
    }

    void Update()
    {
        if (canvasPadre == null) return;

        // Si el juego terminó (Panel de Game Over o Panel de Victoria activos), la gaviota y su sonido se pausan
        GameObject panelGameOver = GameObject.Find("PanelGameOver");
        GameObject panelVictoria = GameObject.Find("PanelVictoria");

        bool juegoTerminado = (panelGameOver != null && panelGameOver.activeSelf) ||
                              (panelVictoria != null && panelVictoria.activeSelf);

        if (juegoTerminado)
        {
            if (audioSourceGaviota != null && audioSourceGaviota.isPlaying)
            {
                audioSourceGaviota.Pause();
            }
            return; // Detiene el movimiento de la gaviota
        }

        // Si el audio estaba pausado y el juego continúa, lo reanudamos
        if (audioSourceGaviota != null && !audioSourceGaviota.isPlaying)
        {
            audioSourceGaviota.UnPause();
        }

        // 1. Mover la gaviota hacia la izquierda continuamente usando píxeles de UI
        rectTransform.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;

        // 2. Obtener el ancho actual del Canvas adaptado a cualquier pantalla
        float anchoCanvas = canvasPadre.GetComponent<RectTransform>().rect.width;

        // 3. Calcular límites izquierdo y derecho basados en el tamaño del Canvas
        float limiteIzquierdo = -(anchoCanvas / 2f) - margenSalida;
        float limiteDerecho = (anchoCanvas / 2f) + margenSalida;

        // 4. Si la gaviota cruza completamente el límite izquierdo, se regenera en el lado derecho
        if (rectTransform.anchoredPosition.x < limiteIzquierdo)
        {
            rectTransform.anchoredPosition = new Vector2(limiteDerecho, rectTransform.anchoredPosition.y);
        }
    }
}