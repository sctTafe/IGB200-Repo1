using UnityEngine;

/// <summary>
/// 
/// DOSE: Handels the regenoration of energy when a team members is not in the mission party (when a day passes) 
/// 
/// </summary>
public class TeamMember_StatsRegeneration_PersistentSingletonMng : MonoBehaviour
{

    #region Singelton Setup
    private static TeamMember_StatsRegeneration_PersistentSingletonMng _instance;
    public static TeamMember_StatsRegeneration_PersistentSingletonMng Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("TeanMember_SelectionGroupHolder_Mng Instance is not found.");
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

    [SerializeField] float _dailyEnergyRegen_Pct = 0.05f;
    [SerializeField] float _dailyMoraleRegen_Pct = 0.05f;
    private DayCounter_PersistentSingletonMng _DaysMng;
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng _SelectionGroupsMng;

    private void Awake()
    {
        SingeltonSetup();
    }

    public void Start()
    {
        _DaysMng = DayCounter_PersistentSingletonMng.Instance;
        _DaysMng._OnDayChange += Handle_OnDayChnage;
        _SelectionGroupsMng = TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;
    }
    public void OnDestroy()
    {
        if (_DaysMng != null)
            _DaysMng._OnDayChange -= Handle_OnDayChnage;        
    }

    private void Handle_OnDayChnage()
    {
        Handle_DayChange_RegenEnergy();
        Handle_OnDayChange_RegenMorale();
    }

    private void Handle_DayChange_RegenEnergy()
    {
        var teamDic = _SelectionGroupsMng._avalibleTeamMemberPool._teamMembersGroup;

        foreach (var teamMember in teamDic)
        {
            teamMember.Value.fn_AddEnergy(teamMember.Value._maxEnergy * _dailyEnergyRegen_Pct);
        }
    }

    private void Handle_OnDayChange_RegenMorale()
    {
        var teamDic = _SelectionGroupsMng._facilitiesTeamMemberPool._teamMembersGroup;
        if (teamDic != null && teamDic.Count > 0)
        {
            foreach (var teamMember in teamDic)
            {
                teamMember.Value.fn_AddMorale(teamMember.Value._maxEnergy * _dailyMoraleRegen_Pct);
            }
        }
    }

}

