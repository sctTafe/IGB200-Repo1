using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BridgeParts_SO", menuName = "SO/BridgeParts_SO")]

public class BridgeParts_SO : ScriptableObject 
{
    [SerializeField] public int _bridgePartUID;
    [SerializeField] public String _bridgePartName;
}
