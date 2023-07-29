using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScottBarley.IGB200.V1
{
    public class UI_EnableCursor_OnStart : MonoBehaviour
    {
        void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}