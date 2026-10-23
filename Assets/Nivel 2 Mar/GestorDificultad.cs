using UnityEngine;

public class GestorDificultad : MonoBehaviour
{
    [Header("Referencias")]
    public GestorPuntuacion gestorPuntuacion;

    [Header("Configuración de Reto")]
    [Tooltip("Cada cuántos metros sube la dificultad")]
    public int metrosParaSubirDificultad = 30;

    private int nivelDificultad = 1;

    void Update()
    {
        if (gestorPuntuacion == null) return;

        // Calculamos el nivel actual basado en los metros recorridos
        int nuevoNivel = 1 + (gestorPuntuacion.Distancia / metrosParaSubirDificultad);

        if (nuevoNivel > nivelDificultad)
        {
            nivelDificultad = nuevoNivel;
            AplicarAumentoDeDificultad();
        }
    }

    void AplicarAumentoDeDificultad()
    {
        Debug.Log("¡Dificultad aumentada al nivel: " + nivelDificultad + "!");

        // Aquí puedes conectar tus generadores para que aumenten su velocidad o frecuencia de aparición
    }
}