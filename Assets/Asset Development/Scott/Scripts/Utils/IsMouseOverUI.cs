using UnityEngine;
using UnityEngine.EventSystems;

public class IsMouseOverUI : MonoBehaviour
{
    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            Debug.Log("Mouse is over a UI element.");
        }
    }
}
