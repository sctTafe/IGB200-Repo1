using System;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// IS: Singelton
/// Description:
///     Project Points Manager
/// 
/// </summary>
public class ProjectPoints_PersistentSingletonMng : MonoBehaviour
{
    #region Singelton Setup
    private static ProjectPoints_PersistentSingletonMng _instance;
    public static ProjectPoints_PersistentSingletonMng Instance {
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

    // - Events -
    public UnityEvent _OnProjectPointsChange;
    public Action<int> _OnProjectPointsChange_CurrentPoints;

    private int _currentProjectPoints;


    private void Awake() {
        SingeltonSetup();
    }

    public void Start()
    {
        _OnProjectPointsChange?.Invoke();
        _OnProjectPointsChange_CurrentPoints?.Invoke(_currentProjectPoints);
    }

    public int fn_GetCurrentPoints()
    {
        return _currentProjectPoints;
    }

    public void fn_AddPoints(int pointsToAdd) {
        if (pointsToAdd > 0)
        {
            _currentProjectPoints += pointsToAdd;
        }
        if (pointsToAdd < 0)
        {
            _currentProjectPoints += pointsToAdd;
            if (_currentProjectPoints < 0)
                _currentProjectPoints = 0;
        }

        _OnProjectPointsChange?.Invoke();
        _OnProjectPointsChange_CurrentPoints?.Invoke(_currentProjectPoints);
    }

    /// <summary>
    /// Used for Player Trying to Purchase Item
    /// </summary>
    /// <param name="pointsToSubtract"></param>
    /// <returns> If the purchase has been successful </returns>
    public bool fn_TrySubtractPoints(int pointsToSubtract) {
        if (_currentProjectPoints >= pointsToSubtract) {
            _currentProjectPoints -= pointsToSubtract;

            _OnProjectPointsChange?.Invoke();
            _OnProjectPointsChange_CurrentPoints?.Invoke(_currentProjectPoints);

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
