using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

/// <summary>
/// 
/// DOSE: Handels the regenoration of energy when a team members is not in the mission party (when a day passes) 
/// 
/// </summary>
public class TeamMember_EnergyRegeneration_PersistentSingletonMng : MonoBehaviour
{

    #region Singelton Setup
    private static TeamMember_EnergyRegeneration_PersistentSingletonMng _instance;
    public static TeamMember_EnergyRegeneration_PersistentSingletonMng Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("TeanMember_SelectionGroupHolder_Mng instance is not found.");
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

    [SerializeField] float _dailyRegenAmountPct = 0.02f; 
    private DayCounter_PersistentSingletonMng _DaysMng;
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng _SelectionGroupsMng;

    private void Awake()
    {
        SingeltonSetup();
    }

    public void Start()
    {
        _DaysMng = DayCounter_PersistentSingletonMng.Instance;
        _DaysMng._OnDayChange += Handle_DayChange;
        _SelectionGroupsMng = TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;
    }
    public void OnDestroy()
    {
        if (_DaysMng != null)
            _DaysMng._OnDayChange -= Handle_DayChange;        
    }

    private void Handle_DayChange()
    {
        var teamDic = _SelectionGroupsMng._avalibleTeamMemberPool._teamMembersGroup;

        foreach (var teamMember in teamDic)
        {
            teamMember.Value.fn_AddEnergy(teamMember.Value._maxEnergy * _dailyRegenAmountPct);
        }
    }

}

