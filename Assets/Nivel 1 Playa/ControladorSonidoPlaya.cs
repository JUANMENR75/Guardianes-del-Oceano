using System.Collections;
using UnityEngine;

public class ControladorSonidoPlaya : MonoBehaviour
{
    public static ControladorSonidoPlaya Instance;

    [Header("Fuentes de Audio")]
    public AudioSource audioSourceMusica;
    public AudioSource audioSourceEfectos;
    public AudioSource audioSourceCaminar;
    public AudioSource audioSourceDepositar; // Canal 100% exclusivo para depositar

    [Header("Clips de Audio")]
    public AudioClip musicaFondo;
    public AudioClip sonidoPinza;
    public AudioClip sonidoGaviota;
    public AudioClip clipCaminar;
    public AudioClip clipDepositar;
    public AudioClip clipGameOver;
    public AudioClip clipVictoria;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        ReproducirMusicaFondo();
    }

    public void ReproducirMusicaFondo()
    {
        if (audioSourceMusica != null && musicaFondo != null)
        {
            // Si la música ya está sonando, NO la reiniciamos para que nunca se corte
            if (audioSourceMusica.clip != musicaFondo || !audioSourceMusica.isPlaying)
            {
                audioSourceMusica.clip = musicaFondo;
                audioSourceMusica.loop = true;
                audioSourceMusica.volume = 0.8f;
                audioSourceMusica.Play();
            }
        }
    }

    public void ReproducirSonidoPinza()
    {
        if (audioSourceEfectos != null && sonidoPinza != null)
        {
            audioSourceEfectos.PlayOneShot(sonidoPinza, 0.3f);
        }
    }

    public void ReproducirSonidoGaviota()
    {
        if (audioSourceEfectos != null && sonidoGaviota != null)
        {
            audioSourceEfectos.PlayOneShot(sonidoGaviota);
        }
    }

    public void ControlarCaminata(bool estaCaminando)
    {
        if (audioSourceCaminar == null || clipCaminar == null) return;

        if (estaCaminando)
        {
            if (!audioSourceCaminar.isPlaying)
            {
                audioSourceCaminar.clip = clipCaminar;
                audioSourceCaminar.loop = true;
                audioSourceCaminar.volume = 1.5f;
                audioSourceCaminar.Play();
            }
        }
        else
        {
            if (audioSourceCaminar.isPlaying)
            {
                audioSourceCaminar.Pause();
            }
        }
    }

    // --- DEPOSITAR: Suena fuerte y claro sin tocar jamás la música de fondo ---
    public void ReproducirSonidoDepositar()
    {
        if (audioSourceDepositar != null && clipDepositar != null)
        {
            audioSourceDepositar.PlayOneShot(clipDepositar, 1.5f);
        }
    }

    public void ActivarSonidoGameOver()
    {
        if (audioSourceMusica != null) audioSourceMusica.Stop();
        if (audioSourceCaminar != null) audioSourceCaminar.Stop();

        if (audioSourceEfectos != null && clipGameOver != null)
        {
            audioSourceEfectos.ignoreListenerPause = true;
            audioSourceEfectos.Stop();
            audioSourceEfectos.clip = clipGameOver;
            audioSourceEfectos.loop = true;
            audioSourceEfectos.volume = 1.0f;
            audioSourceEfectos.Play();
        }
    }

    public void ActivarSonidoVictoria()
    {
        if (audioSourceMusica != null) audioSourceMusica.Stop();
        if (audioSourceCaminar != null) audioSourceCaminar.Stop();

        if (audioSourceEfectos != null && clipVictoria != null)
        {
            audioSourceEfectos.ignoreListenerPause = true;
            audioSourceEfectos.Stop();
            audioSourceEfectos.clip = clipVictoria;
            audioSourceEfectos.loop = false;
            audioSourceEfectos.volume = 1.0f;
            audioSourceEfectos.Play();

            StartCoroutine(EsperarFinVictoriaYReanudarMusica(clipVictoria.length));
        }
    }

    private IEnumerator EsperarFinVictoriaYReanudarMusica(float duracion)
    {
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.unscaledDeltaTime;
            yield return null;
        }
        ReproducirMusicaFondo();
    }

    public void PausarTodoElAudio() { } // Vacío para evitar interferencias en juego normal
    public void ReanudarTodoElAudio() { ReproducirMusicaFondo(); }

    public void DetenerTodoElAudio()
    {
        if (audioSourceMusica != null) audioSourceMusica.Stop();
        if (audioSourceEfectos != null) audioSourceEfectos.Stop();
        if (audioSourceCaminar != null) audioSourceCaminar.Stop();
        if (audioSourceDepositar != null) audioSourceDepositar.Stop();
    }
}