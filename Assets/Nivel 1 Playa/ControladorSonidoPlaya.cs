using UnityEngine;

public class ControladorSonidoPlaya : MonoBehaviour
{
    public static ControladorSonidoPlaya Instance;

    [Header("Fuentes de Audio")]
    public AudioSource audioSourceMusica;
    public AudioSource audioSourceEfectos;

    [Header("Clips de Audio")]
    public AudioClip musicaFondo;
    public AudioClip sonidoPinza;
    public AudioClip sonidoGaviota;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (audioSourceMusica != null && musicaFondo != null)
        {
            audioSourceMusica.clip = musicaFondo;
            audioSourceMusica.loop = true;
            audioSourceMusica.Play();
        }
    }

    public void ReproducirSonidoPinza()
    {
        if (audioSourceEfectos != null && sonidoPinza != null)
        {
            audioSourceEfectos.PlayOneShot(sonidoPinza);
        }
    }

    public void ReproducirSonidoGaviota()
    {
        if (audioSourceEfectos != null && sonidoGaviota != null)
        {
            audioSourceEfectos.PlayOneShot(sonidoGaviota);
        }
    }

    // --- MÉTODO FALTANTE QUE CAUSABA EL ERROR ---
    public void PausarTodoElAudio()
    {
        if (audioSourceMusica != null) audioSourceMusica.Pause();
        if (audioSourceEfectos != null) audioSourceEfectos.Pause();
    }

    public void ReanudarTodoElAudio()
    {
        if (audioSourceMusica != null) audioSourceMusica.UnPause();
        if (audioSourceEfectos != null) audioSourceEfectos.UnPause();
    }

    public void DetenerTodoElAudio()
    {
        if (audioSourceMusica != null) audioSourceMusica.Stop();
        if (audioSourceEfectos != null) audioSourceEfectos.Stop();
    }
}