using System;

[Serializable]
public class MouseSelectionInformation_Item
{
    public Action OnChange;
    public string _name;
    public int _upgradeCost;

    public bool _isPurchasable;
    public bool _isPurchased;
    public bool _isVisable;

    public void fn_Purchase()
    {
        _isPurchased = true;
        OnChange?.Invoke();
    }
}