using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUI_Mng : MonoBehaviour
{
    [SerializeField] private Selection_UI _template;
    [SerializeField] private GameObject _panelRoot;
    private Transform _panelRoot_Transform;

    //dictionary for binding the team members to, by unique ID
    private Dictionary<int, Selection_UI> _holders = new Dictionary<int, Selection_UI>();
    private SelectionGroup _boundSelectionGroup;

    private void Awake() {
        _panelRoot_Transform = _panelRoot.transform;
        _panelRoot.SetActive(false);
        _template.gameObject.SetActive(false);
    }


    public void fn_Bind(SelectionGroup selectionGroup)
    {

    }


}
