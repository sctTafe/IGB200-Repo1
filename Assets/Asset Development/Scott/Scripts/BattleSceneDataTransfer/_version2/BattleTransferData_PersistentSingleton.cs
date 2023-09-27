using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BattleTransferData_PersistentSingleton : MonoBehaviour
{

    #region Singelton Setup
    private static BattleTransferData_PersistentSingleton _instance;
    public static BattleTransferData_PersistentSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("DayCounter_Mng instance is not found.");
            }
            return _instance;
        }
    }
    private void SingeltonSetup()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public static List<GameObject> missionTeam = new();
    public int _currentMissionID;
    public bool _isMissionCompletedSuccessfully;
    

    void Awake()
    {
        SingeltonSetup();
    }

}
