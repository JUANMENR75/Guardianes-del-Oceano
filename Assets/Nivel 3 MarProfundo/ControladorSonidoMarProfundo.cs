using System.Collections;
using UnityEngine;

public class ControladorSonidoMarProfundo : MonoBehaviour
{
    public static ControladorSonidoMarProfundo Instance;

    [Header("Fuentes de Audio")]
    public AudioSource audioSourceMusica;
    public AudioSource audioSourceEfectos;

    [Header("Clips de Audio")]
    public AudioClip musicaFondo;
    public AudioClip clipPilaVerde;
    public AudioClip clipPilaRoja;
    public AudioClip clipBasura;
    public AudioClip clipVictoria;
    public AudioClip clipGameOver;

    private Coroutine corrutinaVictoria;

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
            if (audioSourceMusica.clip != musicaFondo || !audioSourceMusica.isPlaying)
            {
                audioSourceMusica.clip = musicaFondo;
                audioSourceMusica.loop = true;
                audioSourceMusica.volume = 0.8f;
                audioSourceMusica.Play();
            }
        }
    }

    public void ReproducirSonidoPilaVerde()
    {
        if (audioSourceEfectos != null && clipPilaVerde != null)
        {
            audioSourceEfectos.PlayOneShot(clipPilaVerde);
        }
    }

    public void ReproducirSonidoPilaRoja()
    {
        if (audioSourceEfectos != null && clipPilaRoja != null)
        {
            audioSourceEfectos.PlayOneShot(clipPilaRoja);
        }
    }

    public void ReproducirSonidoBasura()
    {
        if (audioSourceEfectos != null && clipBasura != null)
        {
            audioSourceEfectos.PlayOneShot(clipBasura);
        }
    }

    public void ActivarSonidoVictoria()
    {
        if (audioSourceMusica != null) audioSourceMusica.Stop();

        if (corrutinaVictoria != null)
        {
            StopCoroutine(corrutinaVictoria);
        }

        if (audioSourceEfectos != null && clipVictoria != null)
        {
            audioSourceEfectos.ignoreListenerPause = true;
            audioSourceEfectos.Stop();
            audioSourceEfectos.clip = clipVictoria;
            audioSourceEfectos.loop = false;
            audioSourceEfectos.volume = 1.0f;
            audioSourceEfectos.Play();

            corrutinaVictoria = StartCoroutine(EsperarFinVictoriaYReanudarMusica(clipVictoria.length));
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

    public void ActivarSonidoGameOver()
    {
        if (audioSourceMusica != null) audioSourceMusica.Stop();

        if (corrutinaVictoria != null)
        {
            StopCoroutine(corrutinaVictoria);
        }

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

    public void DetenerTodoElAudio()
    {
        if (corrutinaVictoria != null)
        {
            StopCoroutine(corrutinaVictoria);
        }
        if (audioSourceMusica != null) audioSourceMusica.Stop();
        if (audioSourceEfectos != null) audioSourceEfectos.Stop();
    }
}