using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Tween scaleTween;

    [Header("Effect Settings")]
    private float scaleMultiplier = 1.08f;
    private float scaleDuration = 0.25f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale Up
        scaleTween?.Kill();
        scaleTween = rectTransform.DOScale(originalScale * scaleMultiplier, scaleDuration).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        scaleTween?.Kill();

        // Reset
        rectTransform.DOScale(originalScale, scaleDuration).SetEase(Ease.OutQuad);
    }
}
