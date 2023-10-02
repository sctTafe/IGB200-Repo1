using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressionInteractableObjects_Building : MonoBehaviour
{
    [SerializeField] public BridgePartsDataHolder _localBridgePartsDataHolder;
    private GameProgressionInteractableObjects_PersistentSingletonMng _GameProgressionObjectsMng;
    private GameObject _thisGameObject;

    #region Unity Native Functions
    private void Awake() {
        _thisGameObject = this.gameObject;

    }
    void Start()
    {
        //_thisGameObject.SetActive(false);
        Setup_ConnectToProgressionInteractableObjectsMng();
        Setup_TryConnectToLocalBridgePartDataHolder();
        CheckEnableStatusOfObject();
    }

    private void Setup_ConnectToProgressionInteractableObjectsMng()
    {
        _GameProgressionObjectsMng ??= GameProgressionInteractableObjects_PersistentSingletonMng.Instance;
        //if (_GameProgressionObjectsMng != null) {
        //    _GameProgressionObjectsMng._OnChange_UpdateOfMissionObjectsActiveList += HandleOnUpdateOfActiveListChange;
        //}
    }

    private void CheckEnableStatusOfObject()
    {
        if (_GameProgressionObjectsMng == null)
            Setup_ConnectToProgressionInteractableObjectsMng();

        _thisGameObject.SetActive(_GameProgressionObjectsMng.fn_Get_IsBuildingGOEnabled(
            _localBridgePartsDataHolder.fn_GetMissionUID()));
    }

    private void Setup_TryConnectToLocalBridgePartDataHolder()
    {
        if (TryGetComponent<BridgePartsDataHolder>(out BridgePartsDataHolder bPDH))
            _localBridgePartsDataHolder = bPDH;

        if (_localBridgePartsDataHolder == null)
            Debug.LogError("GameProgressionInteractableObjects_Building: Setup_TryConnectToLocalBridgePartDataHolder; ERROR - Couldn't connect to local BridgePartsDataHolder!");
    }

    #endregion



}

