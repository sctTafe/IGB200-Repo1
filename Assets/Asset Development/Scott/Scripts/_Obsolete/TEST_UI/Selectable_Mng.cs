using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Selectable_Mng : MonoBehaviour
{
    [SerializeField] private Selectable_UI _slectableUI_Template;
    [SerializeField] private GameObject _slectableUI_PanelRoot;
    private Transform _slectableUI_PanelRootTransform;

    //dictionary for binding the team members to, by unique ID
    private Dictionary<int, Selectable_UI> _UIElementsHolder_Dic = new Dictionary<int, Selectable_UI>();
    private SelectionGroup _boundSelectionGroup;

    private void Awake() {
        _slectableUI_PanelRootTransform = _slectableUI_PanelRoot.transform;
        _slectableUI_Template.gameObject.SetActive(false);

        //_slectableUI_PanelRoot.SetActive(false);

    }

    private void Start()
    {
        #region Testing Only

        int numberToCreate = 5;
        List<Selectable> selectables = new();


        for (int i = 0; i < numberToCreate; i++) {
            //create new selectable
            Selectable selectable = new Selectable();
            selectable._name = i.ToString();
            selectable._uID = i;

            UISlot_CreateSelectableUIElement(selectable);
        }
        #endregion

    }       

    private void UISlot_CreateSelectableUIElement(Selectable selectable)
    {
        Selectable_UI newSelectableUI = Instantiate(_slectableUI_Template, _slectableUI_PanelRootTransform);
        newSelectableUI.fn_Bind(selectable,this);
        newSelectableUI.gameObject.SetActive(true);
        _UIElementsHolder_Dic.Add(selectable._uID,newSelectableUI);
    }

    private void UISlot_RemoveUISlot(int slectable_UID)
    {
        if (_UIElementsHolder_Dic.TryGetValue(slectable_UID, out Selectable_UI selectableUI))
        {
            Destroy(selectableUI.gameObject); //DOSE: Destroy the UI element game object
            _UIElementsHolder_Dic.Remove(slectable_UID); //DOSE: Sets Dictionary Entry for the key to null, garbage collector deals with the unreferenced data
        }

    }


    public void fn_SelectToAddToTeam(Selectable selectable)
    {
        UISlot_RemoveUISlot(selectable._uID);
    }





    public void fn_Bind(SelectionGroup selectionGroup)
    {
    }


}
