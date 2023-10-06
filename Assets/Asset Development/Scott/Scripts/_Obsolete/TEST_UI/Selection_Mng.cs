using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Mng : MonoBehaviour
{
    List<Selectable> _SelectableList;


}

public class SelectionGroup
{
    [SerializeField] public event Action OnGroupChange;
    public List<Selectable> _slectablesInGroup { get; private set; } = new();
}