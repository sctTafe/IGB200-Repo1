using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// 
/// 
/// </summary>
public class GameProgression_InteractableObjects_Mng : MonoBehaviour
{
    public GameObject[] _InteractableProgressionGameObjects;

    private Dictionary<int, GameObject> _InteractableProgressionGOs_Dic = new Dictionary<int, GameObject>();
    private bool _isSetUpComplete = false;
    private int _UID;
    void Start()
    {
        if (!_isSetUpComplete)
        {
            Setup_LoadObjectsIntoDictionary();
        }
    }

    private void Setup_LoadObjectsIntoDictionary()
    {
        foreach (var go in _InteractableProgressionGameObjects)
        {
            GameProgression_InteractableObjects_InfoHolder info =
                go.GetComponent<GameProgression_InteractableObjects_InfoHolder>();
            info._ID = UID_GetNext();

            _InteractableProgressionGOs_Dic.TryAdd(info._ID, go);
        }
        _isSetUpComplete = true;
    }

    private int UID_GetNext()
    {
        return _UID++;
    }
}
