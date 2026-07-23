using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; // Importamos DOTween

public class AnimacionHerramientas : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 escalaOriginal;
    private float alturaOriginalY;

    void Start()
    {
        // Guardamos la posición y escala inicial del botón o cuadro
        escalaOriginal = transform.localScale;
        alturaOriginalY = transform.localPosition.y;
    }

    // Se ejecuta cuando el cursor PASA por encima (Hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Detenemos animaciones previas para evitar tirones
        transform.DOKill();

        // Efecto Pro: Crece un 15% y se eleva suavemente hacia arriba en el eje Y
        transform.DOScale(escalaOriginal * 1.15f, 0.2f).SetEase(Ease.OutBack);
        transform.DOLocalMoveY(alturaOriginalY + 10f, 0.2f).SetEase(Ease.OutQuad);
    }

    // Se ejecuta cuando RETIRAS el cursor de encima
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();

        // Regresa suavemente a su tamaño y posición original
        transform.DOScale(escalaOriginal, 0.2f).SetEase(Ease.OutQuad);
        transform.DOLocalMoveY(alturaOriginalY, 0.2f).SetEase(Ease.OutQuad);
    }

    // Se ejecuta cuando HACES CLIC en el botón/flecha
    public void OnPointerClick(PointerEventData eventData)
    {
        // Microefecto visual de "rebote" al hacer clic
        transform.DOKill();
        transform.DOScale(escalaOriginal * 0.9f, 0.08f).SetLoops(2, LoopType.Yoyo);
    }
}