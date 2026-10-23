using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;

public class Monitoreo : MonoBehaviour
{
    // 1. Pega aquí tu nueva API Key de 32 caracteres (sin guiones)
    public string apiKey = "49d4c77d419544ddcd4ce1811e51b58f";

    // 2. URL corregida para tu sitio actual (datadoghq.com sin us3)
    public string apiUrl = "https://api.datadoghq.com/api/v2/series";

    void Start()
    {
        // Reportamos que alguien abrió el juego
        StartCoroutine(EnviarMetrica("toxichorizon.sesiones", 1));
    }

    void Update()
    {
        // Monitoreo de rendimiento: Envía los FPS aprox. cada 5 segundos
        if (Time.frameCount % 300 == 0)
        {
            float fps = 1.0f / Time.deltaTime;
            StartCoroutine(EnviarMetrica("toxichorizon.rendimiento.fps", Mathf.RoundToInt(fps)));
        }
    }

    public void ReportarMuerte()
    {
        StartCoroutine(EnviarMetrica("toxichorizon.jugador.muertes", 1));
    }

    IEnumerator EnviarMetrica(string nombreMetrica, float valor)
    {
        long unixTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

        string json = $@"{{
            ""series"": [{{
                ""metric"": ""{nombreMetrica}"",
                ""type"": 3,
                ""points"": [{{ ""timestamp"": {unixTime}, ""value"": {valor} }}]
            }}]
        }}";

        using (UnityWebRequest web = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            web.uploadHandler = new UploadHandlerRaw(bodyRaw);
            web.downloadHandler = new DownloadHandlerBuffer();

            web.SetRequestHeader("Content-Type", "application/json");
            web.SetRequestHeader("DD-API-KEY", apiKey);

            yield return web.SendWebRequest();

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[Datadog Error] {web.responseCode}: {web.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"[Datadog OK] Métrica enviada: {nombreMetrica} = {valor}");
            }
        }
    }
}