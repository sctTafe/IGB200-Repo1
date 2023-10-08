using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains information that goes between Progression and Battle Scene
public class StaticData : MonoBehaviour
{
    public static List<TeamMemberTransfer_Data> team = new();
    public static GameObject playerOne;
    public static int battlesPlayed = 0;

    // - Used In Progress Scene
    public static bool isBattleWon = false;
    public static int currentMissionID;         // Holds the Mission ID for when its needed on return to the progress scene
    public static int additionalProjectPointsEarned;
    public static bool isBattleGameManagerInTestModeOverride;
}
public enum EnemyTypes {
    error,
    Water,      // pipe
    Electric,   // wire
    Practical,  // blocks
    Planning,   // scaffolding  
    Special     // special missions - not added yet (maybe 1,2,3, etc.)
}