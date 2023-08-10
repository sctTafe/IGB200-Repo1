using UnityEngine;

/// <summary>
/// Name:
///     IMC_BridgeSections
/// Requirements:
///     Part of IMouseClickable System
/// </summary>
public class IMC_BridgeSections : MonoBehaviour, IMouseClickable
{
    public void OnCMouseClick()
    {
        Debug.Log("Bridge Section Clicked");
    }
}
