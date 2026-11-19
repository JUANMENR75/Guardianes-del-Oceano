using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;

public class Monitoreo : MonoBehaviour
{
    public string apiKey = "49d4c77d419544ddcd4ce1811e51b58f";
    public string apiUrl = "https://api.datadoghq.com/api/v2/series";

    private float tiempoAcumulado = 0f;
    private int framesAcumulados = 0;
    private const float intervaloEnvioSegundos = 5f;

    void Start()
    {
        StartCoroutine(EnviarMetrica("toxichorizon.sesiones", 1));
    }

    void Update()
    {
        tiempoAcumulado += Time.unscaledDeltaTime;
        framesAcumulados++;

        if (tiempoAcumulado >= intervaloEnvioSegundos)
        {
            float fpsPromedio = framesAcumulados / tiempoAcumulado;
            StartCoroutine(EnviarMetrica("toxichorizon.rendimiento.fps", Mathf.RoundToInt(fpsPromedio)));

            tiempoAcumulado = 0f;
            framesAcumulados = 0;
        }
    }

    public void ReportarMuerte()
    {
        StartCoroutine(EnviarMetrica("toxichorizon.jugador.muertes", 1));
    }

    IEnumerator EnviarMetrica(string nombreMetrica, float valor)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            yield break;
        }

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
            web.timeout = 5;

            web.SetRequestHeader("Content-Type", "application/json");
            web.SetRequestHeader("DD-API-KEY", apiKey);

            yield return web.SendWebRequest();

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"[Datadog Aviso] {web.responseCode}: {web.error}");
            }
            else
            {
                Debug.Log($"[Datadog OK] Métrica enviada: {nombreMetrica} = {valor}");
            }
        }
    }
}