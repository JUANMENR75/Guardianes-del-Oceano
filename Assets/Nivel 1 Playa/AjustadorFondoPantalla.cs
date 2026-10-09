using UnityEngine;

public class AjustadorFondoPantalla : MonoBehaviour
{
    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        float alturaCamara = cam.orthographicSize * 2.0f;
        float anchoCamara = alturaCamara * cam.aspect;

        Vector2 tamanoSprite = sr.sprite.rect.size / sr.sprite.pixelsPerUnit;

        float escalaX = anchoCamara / tamanoSprite.x;
        float escalaY = alturaCamara / tamanoSprite.y;
        float escalaFinal = Mathf.Max(escalaX, escalaY) * 1.15f;

        transform.localScale = new Vector3(escalaFinal, escalaFinal, 1f);
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
    }
}