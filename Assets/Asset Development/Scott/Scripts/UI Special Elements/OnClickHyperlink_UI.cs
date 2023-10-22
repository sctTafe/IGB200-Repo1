using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickHyperlink_UI : MonoBehaviour
{
    [SerializeField] private string _link;

    public void fn_HandleOnClick()
    {
        Application.OpenURL(_link);
    }
}
