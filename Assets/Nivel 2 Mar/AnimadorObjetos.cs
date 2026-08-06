using UnityEngine;
using DG.Tweening;

public class AnimadorObjetos : MonoBehaviour
{
    public enum Tipo { Moneda, Basura, Bomba }
    public Tipo tipoObjeto;

    void Start()
    {
        switch (tipoObjeto)
        {
            case Tipo.Moneda:
                // Giro suave infinito
                transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360)
                         .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                break;

            case Tipo.Basura:
                // Balanceo
                transform.DORotate(new Vector3(0, 0, 25f), 2.5f)
                         .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                // Flotación suave
                transform.DOMoveY(transform.position.y + 0.15f, 1.2f)
                         .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
                break;

            case Tipo.Bomba:
                // Escala de latido
                transform.DOScale(transform.localScale * 1.15f, 0.5f)
                         .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
                break;
        }
    }
}