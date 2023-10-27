using UnityEngine;

/// <summary>
/// 
/// DOSE:
///     Closes the games active persistent singletons,with the intention of allowing them to start a fresh when the game is reset
/// NOTE:
///     Only Use in the start menu screen
/// </summary>

public class GameReset_Mng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // - Scene Transfer Data Holders -
        if (DataTransfer_PersistentSingletonMng.Instance != null)
            Destroy(DataTransfer_PersistentSingletonMng.Instance.gameObject);

        if (BattleTransferData_PersistentSingleton.Instance != null)
            Destroy(BattleTransferData_PersistentSingleton.Instance.gameObject);
        
        // - Progression Scene Data Holders - 

        if (DayCounter_PersistentSingletonMng.Instance != null)
            Destroy(DayCounter_PersistentSingletonMng.Instance.gameObject);

        if (ProjectPoints_PersistentSingletonMng.Instance != null)
            Destroy(ProjectPoints_PersistentSingletonMng.Instance.gameObject);

        if (GameProgressionInteractableObjects_PersistentSingletonMng.Instance != null)
            Destroy(GameProgressionInteractableObjects_PersistentSingletonMng.Instance.gameObject);

        if (TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance != null)
            Destroy(TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance.gameObject);

        if (TeamMember_StatsRegeneration_PersistentSingletonMng.Instance != null)
            Destroy(TeamMember_StatsRegeneration_PersistentSingletonMng.Instance.gameObject);
    }


}
