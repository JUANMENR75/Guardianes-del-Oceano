using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SonidosEscena1 : MonoBehaviour
{
    public static SonidosEscena1 instance;

    [Header("Audios Ambientales (Loop)")]
    public AudioSource audioMar;
    public AudioSource audioBarco;

    [Header("Efectos de Sonido (SFX)")]
    public AudioSource efectosAudioSource;
    public AudioSource audioGameOverSource;
    public AudioClip clipMoneda;
    public AudioClip clipBasura;
    public AudioClip clipExplosionBomba;
    public AudioClip clipGameOver;
    public AudioClip clipClicBoton;

    private bool juegoTerminado = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ReproducirAmbientesJuego();
    }

    void ReproducirAmbientesJuego()
    {
        juegoTerminado = false;

        if (audioMar != null && !audioMar.isPlaying) { audioMar.loop = true; audioMar.Play(); }
        if (audioBarco != null && !audioBarco.isPlaying) { audioBarco.loop = true; audioBarco.Play(); }
    }

    public void ReproducirSonidoMoneda()
    {
        if (efectosAudioSource != null && clipMoneda != null && !juegoTerminado)
        {
            efectosAudioSource.PlayOneShot(clipMoneda);
        }
    }

    public void ReproducirSonidoBasura()
    {
        if (efectosAudioSource != null && clipBasura != null && !juegoTerminado)
        {
            efectosAudioSource.PlayOneShot(clipBasura);
        }
    }

    public void TriggerGameOver()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        // Apagamos los sonidos ambientales de inmediato
        if (audioMar != null) audioMar.Stop();
        if (audioBarco != null) audioBarco.Stop();

        // 1. La bomba suena al natural y de forma instantánea
        if (efectosAudioSource != null && clipExplosionBomba != null)
        {
            efectosAudioSource.PlayOneShot(clipExplosionBomba);
        }

        // 2. Damos un respiro corto de 0.4 segundos para que la bomba inicie perfecta antes del Game Over
        StartCoroutine(EsperarYSonarGameOver());
    }

    IEnumerator EsperarYSonarGameOver()
    {
        yield return new WaitForSecondsRealtime(0.4f);

        AudioSource sourceAGuiar = audioGameOverSource != null ? audioGameOverSource : efectosAudioSource;

        if (sourceAGuiar != null && clipGameOver != null)
        {
            sourceAGuiar.PlayOneShot(clipGameOver);
        }
    }

    public void ReproducirClicYReiniciar(string nombreEscenaJuego)
    {
        Time.timeScale = 1f;

        if (efectosAudioSource != null && clipClicBoton != null)
        {
            efectosAudioSource.PlayOneShot(clipClicBoton);
        }

        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void ReproducirClicYIrAlMenu(string nombreEscenaMenu)
    {
        Time.timeScale = 1f;

        if (efectosAudioSource != null && clipClicBoton != null)
        {
            efectosAudioSource.PlayOneShot(clipClicBoton);
        }

        SceneManager.LoadScene(nombreEscenaMenu);
    }
}