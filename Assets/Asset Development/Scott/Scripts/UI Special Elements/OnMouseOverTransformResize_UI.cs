using UnityEngine;
using DG.Tweening;

public class OnMouseOverTransformResize_UI : MonoBehaviour
{
    [SerializeField] private float scalePercentage = 1.5f;
    [SerializeField] private float duration = 0.3f;

    private Vector3 initialScale;
    private Vector3 targetScale;

    private void Start() {
        initialScale = transform.localScale;
        targetScale = initialScale * scalePercentage;
    }

    private void OnMouseEnter() {
        transform.DOScale(targetScale, duration);
    }

    private void OnMouseExit() {
        transform.DOScale(initialScale, duration);
    }
}