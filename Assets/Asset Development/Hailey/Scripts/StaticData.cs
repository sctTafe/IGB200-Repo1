using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains information that goes between Progression and Battle Scene
public class StaticData : MonoBehaviour
{
    public static List<TeamMemberTransfer_Data> team = new();
    public static GameObject playerOne;
    
    public static bool isBattleWon = false;
}
