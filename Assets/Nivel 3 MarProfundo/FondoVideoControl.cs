using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class FondoVideoControl : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true; // Forzamos el bucle continuo sin cortes
        videoPlayer.frame = 0;
        videoPlayer.Prepare();
    }

    void Start()
    {
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.prepareCompleted += (vp) => vp.Play();
        }
    }

    void Update()
    {
        GameObject panelGameOver = GameObject.Find("PanelGameOver");
        GameObject panelVictoria = GameObject.Find("PanelVictoria");

        bool juegoTerminado = (panelGameOver != null && panelGameOver.activeSelf) ||
                              (panelVictoria != null && panelVictoria.activeSelf);

        // Solo pausamos si hay pantalla de Game Over o Victoria, de lo contrario dejamos que corra continuo y fluido
        if (juegoTerminado)
        {
            if (videoPlayer.isPlaying) videoPlayer.Pause();
        }
        else
        {
            if (!videoPlayer.isPlaying && videoPlayer.isPrepared) videoPlayer.Play();
        }
    }
}