using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePartsDataHolder : MonoBehaviour
{
    [SerializeField] public BridgeParts_SO _BridgePartSO;


    public int fn_GetMissionUID() {
        return _BridgePartSO._bridgePartUID;
    }
}
