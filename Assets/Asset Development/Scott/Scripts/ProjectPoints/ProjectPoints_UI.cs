using UnityEngine;
using TMPro;

/// <summary>
/// 
/// Description:
///     Connects to 'ProjectPoints Persistent Singleton' on Awake, and updated based on event triggers from it
///     
/// </summary>
public class ProjectPoints_UI : MonoBehaviour
{

    [SerializeField] private TMP_Text _projectPointsTMP;

    ProjectPoints_PersistentSingletonMng _ProjectPointsManager;

    void Awake()
    {
        ConnectToProjectPointsMng();
        if (_ProjectPointsManager != null)
        {
            // set to current day on start
            fn_UpdateCurrentPoints(_ProjectPointsManager.fn_GetCurrentPoints());
        }
    }
    private void OnDestroy()
    {
        if (_ProjectPointsManager != null)
            _ProjectPointsManager._OnProjectPointsChange_CurrentPoints -= HandleOnProjectPointsChange;
    }

    void fn_UpdateCurrentPoints(int day)
    {
        _projectPointsTMP.SetText(day.ToString());
    }

    private void ConnectToProjectPointsMng()
    {
        _ProjectPointsManager ??= ProjectPoints_PersistentSingletonMng.Instance;
        if (_ProjectPointsManager != null)
        {
            _ProjectPointsManager._OnProjectPointsChange_CurrentPoints += HandleOnProjectPointsChange;
        }
    }

    private void HandleOnProjectPointsChange(int day)
    {
        fn_UpdateCurrentPoints(day);
    }

}
