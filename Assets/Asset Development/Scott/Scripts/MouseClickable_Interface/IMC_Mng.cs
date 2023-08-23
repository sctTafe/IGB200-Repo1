using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Name: IMouseClickable Manager
/// Dose: Shoots out a screen ray on mouse(0) click and checks for a collision with an object using the IMouseClickable Interface & calls its method
/// </summary>
public class IMC_Mng : MonoBehaviour
{

    public UnityEvent OnNullClick;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            bool mouseIsOverUI = EventSystem.current.IsPointerOverGameObject();

            if (Physics.Raycast(ray, out hit))
            {
                IMouseClickable[] iClickables = hit.transform.GetComponents<IMouseClickable>();

                // DOSE: If mouse not over UI & is over a iClickable Object trigger event
                if (!mouseIsOverUI)
                {
                    if (iClickables != null) {
                        foreach (var clickable in iClickables) {
                            clickable.OnCMouseClick();
                        }
                    }
                }

                // DOSE: If mouse not over UI, and player is clicking but not on a IClickable Object
                if (iClickables == null || iClickables.Length == 0)
                {
                    // only work if mouse not over UI element 
                    if (!EventSystem.current.IsPointerOverGameObject())
                        OnNullClick?.Invoke();

                }
            }
        }
    }
}