using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selection_UI : MonoBehaviour
{
    public TMP_Text _nameText;
    
    private Selectable _selectableCurrent;

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public void fn_TrySelect()
    {

    }

    public void fn_UnSeclect()
    {

    }

    public void fn_Bind(Selectable selectable)
    {
        // UnBind from Old
        Unsubscribe();
        UnbindValues();

        // Bind New
        _selectableCurrent = selectable;

        if (_selectableCurrent != null)
        {
            _selectableCurrent.OnChange += HandleChange_UpdateValues;
            HandleChange_UpdateValues();
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    #region Private Functions
    private void HandleChange_UpdateValues() {
        _nameText.SetText(_selectableCurrent._name);
    }

    private void Unsubscribe() {
        if (_selectableCurrent != null) {
            _selectableCurrent.OnChange -= HandleChange_UpdateValues;
        }
    }
    private void UnbindValues() {
        _nameText.SetText("");
    }
    #endregion


}
