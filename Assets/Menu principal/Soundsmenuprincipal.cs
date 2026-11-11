using UnityEngine;

public class Soundsmenuprincipal : MonoBehaviour
{
    [Header("Fuentes de Audio Ambientales")]
    public AudioSource audioMusica;
    public AudioSource audioMar;
    public AudioSource audioGaviotas;
    public AudioSource audioBarco;

    private void Start()
    {
        ReproducirSonidos();
    }

    void ReproducirSonidos()
    {
        if (audioMusica != null) { audioMusica.loop = true; audioMusica.Play(); }
        if (audioMar != null) { audioMar.loop = true; audioMar.Play(); }
        if (audioGaviotas != null) { audioGaviotas.loop = true; audioGaviotas.Play(); }
        if (audioBarco != null) { audioBarco.loop = true; audioBarco.Play(); }
    }
}