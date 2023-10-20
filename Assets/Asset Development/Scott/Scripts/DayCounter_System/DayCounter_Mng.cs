using UnityEngine;
/// <summary>
///
/// DOSE:
///     Non-persistent interface for talking to the 'DayCounter_PersistentSingletonMng'
/// DEPENDENCY:
///     'ProjectPoints_PersistentSingletonMng'
/// 
/// </summary>

public class DayCounter_Mng : MonoBehaviour
{
    private DayCounter_PersistentSingletonMng _PersistentSingletonMng;
    private ProjectPoints_PersistentSingletonMng _ProjectPointsMng;
    [SerializeField] private int _skipDayCost = 2; 


    private void Start()
    {
        ConnectToMngInstances();

    }
    public void fn_TryRestDay()
    {
        ConnectToMngInstances();

        if (_ProjectPointsMng.fn_TrySubtractPoints(_skipDayCost))
        {
            _PersistentSingletonMng.fn_updateToNextDay();
        }
        
    }

    private void ConnectToMngInstances()
    {
        if (_PersistentSingletonMng == null)
            _PersistentSingletonMng = DayCounter_PersistentSingletonMng.Instance;

        if (_ProjectPointsMng == null)
            _ProjectPointsMng = ProjectPoints_PersistentSingletonMng.Instance;
    }
}
