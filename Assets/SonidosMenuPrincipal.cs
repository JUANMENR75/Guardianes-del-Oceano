using UnityEngine;
using UnityEngine.SceneManagement;

public class SonidosMenuPrincipal : MonoBehaviour
{
    public static SonidosMenuPrincipal instance;

    [Header("Audio Source para Clics")]
    public AudioSource efectosAudioSource;
    public AudioClip clipClicBoton;

    [Header("Audios Ambientales del Menú")]
    public AudioSource audioMusicaFondo;
    public AudioSource audioMar;
    public AudioSource audioGaviotas;
    public AudioSource audioBarco;

    private void Awake()
    {
        // Patrón Singleton para mantener este AudioManager vivo entre escenas de menú/herramientas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        // Nos suscribimos al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Esto se ejecuta cada vez que Unity carga una escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reemplaza "MenuPrincipal" con el nombre exacto de tu escena de menú en Unity
        if (scene.name == "MenuPrincipal")
        {
            ReproducirAmbientes();
        }
        else
        {
            // Si estamos en "Herramientas" o cualquier otra que no sea el menú, se apagan
            DetenerAmbientes();
        }
    }

    void ReproducirAmbientes()
    {
        if (audioMusicaFondo != null && !audioMusicaFondo.isPlaying) { audioMusicaFondo.loop = true; audioMusicaFondo.Play(); }
        if (audioMar != null && !audioMar.isPlaying) { audioMar.loop = true; audioMar.Play(); }
        if (audioGaviotas != null && !audioGaviotas.isPlaying) { audioGaviotas.loop = true; audioGaviotas.Play(); }
        if (audioBarco != null && !audioBarco.isPlaying) { audioBarco.loop = true; audioBarco.Play(); }
    }

    public void ReproducirSonidoClic()
    {
        if (efectosAudioSource != null && clipClicBoton != null)
        {
            efectosAudioSource.PlayOneShot(clipClicBoton);
        }
    }

    void DetenerAmbientes()
    {
        if (audioMusicaFondo != null) audioMusicaFondo.Stop();
        if (audioMar != null) audioMar.Stop();
        if (audioGaviotas != null) audioGaviotas.Stop();
        if (audioBarco != null) audioBarco.Stop();
    }
}