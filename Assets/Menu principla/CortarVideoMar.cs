using UnityEngine;
using UnityEngine.Video;

public class CortarVideoMar : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        // Si el video llega a los 5 segundos, regresa al inicio (0)
        if (videoPlayer.time >= 2f)
        {
            videoPlayer.time = 5f;
        }
    }
}