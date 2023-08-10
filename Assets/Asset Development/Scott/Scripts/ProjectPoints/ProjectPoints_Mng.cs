using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ProjectPoints_Mng : MonoBehaviour
{
    #region Singelton Setup
    private static ProjectPoints_Mng _instance;
    public static ProjectPoints_Mng Instance {
        get {
            if (_instance == null) {
                Debug.LogError("ProjectPoints_Mng instance is not found.");
            }
            return _instance;
        }
    }
    private void SingeltonSetup() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public UnityEvent<int> OnProjectPointsChange;
    [SerializeField] private int _projectPoints;


    private void Awake() {
        SingeltonSetup();
    }

    public void Start()
    {
        OnProjectPointsChange?.Invoke(_projectPoints);
    }

    public void fn_AddPoints(int pointsToAdd) {
        if (pointsToAdd > 0)
        {
            _projectPoints += pointsToAdd;
            OnProjectPointsChange?.Invoke(_projectPoints);
        }
            

    }
    public bool fn_TrySubtractPoints(int pointsToSubtract) {
        if (_projectPoints >= pointsToSubtract) {
            _projectPoints -= pointsToSubtract;
            OnProjectPointsChange?.Invoke(_projectPoints);
            return true;
        }
        return false;
    }


    // Do Not Use: Only For Testing
    [ContextMenu("Testing (Add 1000 Points)")]
    public void test_Add1000Points()
    {
        fn_AddPoints(1000);
    }
}
