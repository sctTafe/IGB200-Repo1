using System;

[Serializable]
public class MouseSelectionInformation_Item
{
    public Action OnChange;
    
    // all
    public string _name;
    
    // - dynamic info -
    public bool _isPurchased;
    public bool _isVisable;

    // - unique info - 
    // purchase Info
    public int _upgradeCost;
    // mission Info
    public int _missionRewardPoints;

    // - classification info -
    //  (used for UI display component, turn on/off)
    public bool _isAMission;
    public bool _isPurchasable;

 
    public void fn_Purchase()
    {
        _isPurchased = true;
        OnChange?.Invoke();
    }
}