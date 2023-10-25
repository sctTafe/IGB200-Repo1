using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
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
                Debug.LogError("DayCounter_Mng Instance is not found.");
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

    // - For Use By Hailey - 
    public static List<GameObject> _missionTeam_List = new();

    // - Battle/Mission Data - 
     
    public int _currentMissionID;
    public bool _isMissionCompletedSuccessfully;
    
    // Bonues Project Points Earned in missions, (added in mission)
    public int _earnedBonuseProjectPoints;

    void Awake()
    {
        SingeltonSetup();
    }

}

