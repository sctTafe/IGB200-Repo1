using UnityEngine;

/// <summary>
/// Description
///     Used for testing
/// </summary>

public class DebugTestPanel_UIAndMng : MonoBehaviour
{
    public void fn_Days_AddDay()
    {
        DayCounter_PersistentSingletonMng.Instance.fn_updateToNextDay();
    }

    public void fn_ProjectPoints_AddPoints()
    {
        ProjectPoints_PersistentSingletonMng.Instance.fn_AddPoints(999);
    }
}
