using UnityEngine;
using UnityEngine.Events;

public class IMC_OnClickUnityEvent : MonoBehaviour, IMouseClickable
{
    public UnityEvent OnClickUnityEvent;

    public void OnCMouseClick()
    {
        OnClickUnityEvent?.Invoke();
    }
}
