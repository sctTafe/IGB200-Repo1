using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class OnPointerCanvasGroupAlphaTween_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _startAlpha = 0.3f;
    [SerializeField] private float _endAlpha = 1f;
    [SerializeField] private float _duration = 0.2f;

    private void Start() {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
        SetAlpha(_startAlpha);
    }

    private void SetAlpha(float alpha) {
        _canvasGroup.alpha = alpha;
    }

    private void TweenAlphaToEndValue() {
        _canvasGroup.DOFade(_endAlpha, _duration).SetEase(Ease.Linear);
    }
    private void TweenAlphaToStartValue() {
        _canvasGroup.DOFade(_startAlpha, _duration).SetEase(Ease.Linear);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        TweenAlphaToEndValue();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TweenAlphaToStartValue();
    }
}
