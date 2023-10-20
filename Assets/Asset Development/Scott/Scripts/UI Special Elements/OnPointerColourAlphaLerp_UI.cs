using DG.Tweening;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnPointerColourAlphaLerp_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField] private TMP_Text[] _TMPTexts;
    [SerializeField] private Image[] _images;
    [SerializeField] private float _alphaValueX = 1f;
    [SerializeField] private float _alphaValueY = 0.2f;
    [SerializeField] private float _duration = 1f;

    private void Start() {
        SetImagesAlpha(_alphaValueY);
        SetTMPTextAlpha(_alphaValueY);
    }

    private void SetImagesAlpha(float alpha) {
        foreach (Image image in _images) {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    private void SetTMPTextAlpha(float alpha)
    {
        foreach (var tmpTxt in _TMPTexts) {
            Color color = tmpTxt.color;
            color.a = alpha;
            tmpTxt.color = color;
        }
    }

    // - To Alpha X Value -
    private void TweenImagesAlpha_ToAlphaValueX() {
        foreach (Image image in _images) {
            image.DOFade(_alphaValueX, _duration).SetEase(Ease.Linear);
        }
    }
    private void TweenTMPTextAlpha_ToAlphaValueX() {
        foreach (var text in _TMPTexts) {
            text.DOFade(_alphaValueX, _duration).SetEase(Ease.Linear);
        }
    }

    // - To Alpha Y Value -
    private void TweenImagesAlpha_ToAlphaValueY() {
        foreach (Image image in _images) {
            image.DOFade(_alphaValueY, _duration).SetEase(Ease.Linear);
        }
    }
    private void TweenTMPTextAlpha_ToAlphaValueY() {
        foreach (var text in _TMPTexts) {
            text.DOFade(_alphaValueY, _duration).SetEase(Ease.Linear);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        TweenImagesAlpha_ToAlphaValueX();
        TweenTMPTextAlpha_ToAlphaValueX();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TweenImagesAlpha_ToAlphaValueY();
        TweenTMPTextAlpha_ToAlphaValueY();
    }

}
