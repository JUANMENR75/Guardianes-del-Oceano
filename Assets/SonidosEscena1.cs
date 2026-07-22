using UnityEngine;
using UnityEngine.SceneManagement;

public class SonidosEscena1 : MonoBehaviour
{
    public static SonidosEscena1 instance;

    [Header("Audios Ambientales (Loop)")]
    public AudioSource audioMar;
    public AudioSource audioBarco;

    [Header("Efectos de Sonido (SFX)")]
    public AudioSource efectosAudioSource;
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

        if (audioMar != null) audioMar.Stop();
        if (audioBarco != null) audioBarco.Stop();

        if (efectosAudioSource != null)
        {
            if (clipExplosionBomba != null) efectosAudioSource.PlayOneShot(clipExplosionBomba);
            if (clipGameOver != null) efectosAudioSource.PlayOneShot(clipGameOver);
        }
    }

    public void ReproducirClicYReiniciar(string nombreEscenaJuego)
    {
        // Restablecemos el tiempo por si estaba pausado o congelado
        Time.timeScale = 1f;

        if (efectosAudioSource != null && clipClicBoton != null)
        {
            efectosAudioSource.PlayOneShot(clipClicBoton);
        }

        // Cargamos la escena directamente
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void ReproducirClicYIrAlMenu(string nombreEscenaMenu)
    {
        // Restablecemos el tiempo por si estaba pausado o congelado
        Time.timeScale = 1f;

        if (efectosAudioSource != null && clipClicBoton != null)
        {
            efectosAudioSource.PlayOneShot(clipClicBoton);
        }

        // Cargamos la escena asegurando el nombre exacto ("MenuPrincipal")
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}