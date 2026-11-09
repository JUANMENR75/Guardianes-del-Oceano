using UnityEngine;

public class Gaviotas : MonoBehaviour
{
    [Header("Configuración de Vuelo")]
    public float velocidad = 300f;
    public float margenSalida = 100f;

    private RectTransform rectTransform;
    private Canvas canvasPadre;
    private AudioSource audioSourceGaviota;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasPadre = GetComponentInParent<Canvas>();

        audioSourceGaviota = GetComponent<AudioSource>();
        if (audioSourceGaviota == null)
        {
            audioSourceGaviota = gameObject.AddComponent<AudioSource>();
        }

        if (ControladorSonidoPlaya.Instance != null && ControladorSonidoPlaya.Instance.sonidoGaviota != null)
        {
            audioSourceGaviota.clip = ControladorSonidoPlaya.Instance.sonidoGaviota;
            audioSourceGaviota.loop = true;
            audioSourceGaviota.volume = 0.12f; // Mucho más bajito y ambiental
            audioSourceGaviota.Play();
        }
    }

    void Update()
    {
        if (canvasPadre == null) return;

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
            return;
        }

        if (audioSourceGaviota != null && !audioSourceGaviota.isPlaying)
        {
            audioSourceGaviota.UnPause();
        }

        rectTransform.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;

        float anchoCanvas = canvasPadre.GetComponent<RectTransform>().rect.width;
        float limiteIzquierdo = -(anchoCanvas / 2f) - margenSalida;
        float limiteDerecho = (anchoCanvas / 2f) + margenSalida;

        if (rectTransform.anchoredPosition.x < limiteIzquierdo)
        {
            rectTransform.anchoredPosition = new Vector2(limiteDerecho, rectTransform.anchoredPosition.y);
        }
    }
}