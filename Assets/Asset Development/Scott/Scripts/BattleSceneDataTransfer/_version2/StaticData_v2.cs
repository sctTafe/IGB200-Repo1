using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class StaticData_v2 : MonoBehaviour
{
    public static List<GameObject> missionTeam = new();
    public int _currentMissionID;
    public bool _missionCompletedSuccessfully;
}
