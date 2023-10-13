using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains information that goes between Progression and Battle Scene
public class StaticData : MonoBehaviour
{
    // - Forward & Back Passed Data - 
    public static List<TeamMemberTransfer_Data> team = new();
    public static int currentMissionID;                             //NOTE: Holds the Mission ID for when its needed on return to the progress scene

    // - Forward Passed Data (Used by Battle Scene) -
    public static int battlePosition = 0;
    public static EnemyTypes enemyType;
    public static bool isBattleGameManagerInTestModeOverride;       //NOTE: used for overriding Mission GameManager 'Test mode' 

    // - Back Passed Data (Used In Progress Scene)
    public static bool isBattleWon = false;
    public static int additionalProjectPointsEarned;                //NOTE: not used currently 

    public static void fn_ClearData()
    {
        team.Clear();
        currentMissionID = 0;
        battlePosition = 0;
        enemyType = EnemyTypes.error;
        isBattleGameManagerInTestModeOverride = false;
        isBattleWon = false;
        additionalProjectPointsEarned = 0;
    }
}
public enum EnemyTypes {
    error,
    Water,      // pipe
    Electric,   // wire
    Practical,  // blocks
    Planning,   // scaffolding  
    Special     // special missions - not added yet (maybe 1,2,3, etc.)
}