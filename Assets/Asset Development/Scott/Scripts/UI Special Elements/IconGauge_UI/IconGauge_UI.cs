using UnityEngine;
using UnityEngine.UI;

public class IconGauge_UI : MonoBehaviour
{
    public Image _Output_Img;
    public Sprite[] _gaugeSprites;
    
    // - private -
    private int numberOfStages;

    void Start()
    {
        numberOfStages = _gaugeSprites.Length;
    }

    public void fn_SetLevel_Pct(float pct) 
    {
        if (numberOfStages < 1)
            numberOfStages = _gaugeSprites.Length;

        int index = Mathf.Clamp(Mathf.FloorToInt(pct * numberOfStages), 0, numberOfStages - 1);
        _Output_Img.sprite = _gaugeSprites[index];
    }

    #region Testing
    // Do Not Use: Only For Testing
    [ContextMenu("Testing ( 5% )")]
    public void test_5() => fn_SetLevel_Pct(0.08f);

    [ContextMenu("Testing ( 15% )")]
    public void test_15() => fn_SetLevel_Pct(0.15f);

    [ContextMenu("Testing ( 25% )")]
    public void test_25() => fn_SetLevel_Pct(0.25f);

    [ContextMenu("Testing ( 50% )")]
    public void test_50() => fn_SetLevel_Pct(0.50f);
    [ContextMenu("Testing ( 85% )")]
    public void test_85() => fn_SetLevel_Pct(0.85f);
    [ContextMenu("Testing ( 95% )")]
    public void test_95() => fn_SetLevel_Pct(0.95f);

    #endregion


}
