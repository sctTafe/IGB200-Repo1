using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selectable_UI : MonoBehaviour
{
    public TMP_Text _nameText;

    private Selectable_Mng _currentMng;
    private Selectable _selectableCurrent;
    

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public void fn_HandleOnSelectionButton()
    {
        _currentMng.fn_SelectToAddToTeam(_selectableCurrent);
    }

    public void fn_UnSeclect()
    {

    }

    public void fn_Bind(Selectable selectable, Selectable_Mng mng)
    {
        // UnBind from Old
        Unsubscribe();
        UnbindValues();

        // Bind New
        _selectableCurrent = selectable;
        _currentMng = mng;

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
