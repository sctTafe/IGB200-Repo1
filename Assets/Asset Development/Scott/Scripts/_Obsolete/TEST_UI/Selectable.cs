using System;
using UnityEngine;

public class Selectable
{
    [SerializeField] public event Action OnChange;
    [SerializeField] public string _name;
    [SerializeField] public int _uID;

    public void fn_TrySelect()
    {
    }
    public void fn_DeSelect()
    {
    }
}
