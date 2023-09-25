using UnityEngine;

/// <summary>
/// 
/// DOSE: 
///     Class for transfering team Member data to battle scene.
///     When 'fn_LoadMissionTeam' is called, TeanMember_SelectionGroupHolder_Mng is contacted, 
///     '_selectedMissionTeam' & its members are retrived, then a holder class is instanciated and added to the  
/// DEPENDENCY: 
///     Calls on 'TeanMember_SelectionGroupHolder_Mng' to get Mission Team
///     Instantiates holder classes, then addes it to the 'team' list in 'StaticData' Class
/// NOTE:
/// 
/// </summary>

public class DataTransfer_PersistentSingletonMng : MonoBehaviour
{

    #region Singelton Setup
    private static DataTransfer_PersistentSingletonMng _instance;
    public static DataTransfer_PersistentSingletonMng Instance
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


    public bool _isDebuggingOn = false;
    public TeamMemberTransfer_Data _Prefab;
    private TeanMember_SelectionGroupHolder_Mng _TeamMemberSelectionGroupHolder_Mng;


    void Awake()
    {
        SingeltonSetup();
    }


    #region Public Functions
    // - Passing Data In -
    public void fn_LoadMissionTeam()
    {
        if (_isDebuggingOn) { Debug.Log("DataTransfer_Mng: fn_LoadMissionTeam Called, "); }

        connectToSelectionGroupHolder();
        if (_TeamMemberSelectionGroupHolder_Mng != null)
        {
            foreach (var item in _TeamMemberSelectionGroupHolder_Mng._selectedMissionTeam._teamMembersGroup.Values)
            {
                // Instantiate New Team Member GameObject
                 GameObject teamMember = InstantiateTeamMemberTransfereHolder(item._uID, item._classType, item._maxEnergy, item._currentEnergy);
                
                // Add to StaticData List
                StaticData_v2.missionTeam.Add(teamMember);
            }
        }
    }
    // - Returning Data Back - 

    public void fn_HandleMissionFinished()
    {
        // Update Mission Outcomes


        // Update Project Points Outcomes


    }




    // - 
    #endregion

    #region Private Functions
    private GameObject InstantiateTeamMemberTransfereHolder(int _uID, TeamMemberClassType _classType, float _maxEnergy, float _currentEnergy)
    {
        TeamMemberTransfer_Data newTeamMemberTransfereData = Instantiate(_Prefab, this.transform);
        newTeamMemberTransfereData._uID = _uID;
        newTeamMemberTransfereData._classType = _classType;
        newTeamMemberTransfereData._maxEnergy = _maxEnergy;
        newTeamMemberTransfereData._currentEnergy = _currentEnergy;
        return newTeamMemberTransfereData.gameObject;
    }
    private void connectToSelectionGroupHolder()
    {
        _TeamMemberSelectionGroupHolder_Mng ??= TeanMember_SelectionGroupHolder_Mng.Instance;

    }
    #endregion



}
