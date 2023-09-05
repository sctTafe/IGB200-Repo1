using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// 
/// Notes: 
/// This is a messy expersnive way of doing it, needs updating to use a coroutine or async
/// 
/// </summary>
public class SliderOutputControl_UI : MonoBehaviour
{
    public Image _fillBar_Img;
    public float _startFillPct = 0.5f;
    public float _fillLerpCoeff = 5f;
    public float _dif;
    public float _currentFill;

    private float _currentTargetValue, _maxValue = 1000;
    private float _barUilerpSpeed;
    private float _targetPct;
    private bool _isLerpModeActive = false;

    private void Start()
    {
        //fn_SetPctFullValue_Pct(_startFillPct);
    }

    private void Update()
    {
        
        LerpBarFiller();
    }

    void LerpBarFiller()
    {
        if (_isLerpModeActive)
        {
            _barUilerpSpeed = _fillLerpCoeff * Time.deltaTime;
            _fillBar_Img.fillAmount = Mathf.Lerp(_fillBar_Img.fillAmount, _currentTargetValue / _maxValue, _barUilerpSpeed);
            
            
            _currentFill = _fillBar_Img.fillAmount;
            _dif = Mathf.Sqrt((_currentFill - _targetPct)* (_currentFill - _targetPct));
            
            if (_dif < 0.001)
                _isLerpModeActive = false;
        }
    }

    public void fn_SetFillPct_Lerp(float fill_pct)
    {
        _currentTargetValue = _maxValue * sanitize_MinMaxValues(fill_pct);
        _targetPct = sanitize_MinMaxValues(fill_pct);
        _isLerpModeActive = true;
    }
    public void fn_SetFillPct_NoLerp(float fill_pct)
    {
        _fillBar_Img.fillAmount = sanitize_MinMaxValues(fill_pct);
        _isLerpModeActive = false;
    }
    private float sanitize_MinMaxValues(float fill_pct)
    {
        if(fill_pct < 0.0f)
            return 0.0f;
        if(fill_pct > 1.0f)
            return 1.0f;
        return fill_pct;
    }




    #region Testing
    // Do Not Use: Only For Testing
    [ContextMenu("Testing ( Set to 25% Lerp )")]
    public void test_SetTo_25Pct() => fn_SetFillPct_Lerp(0.25f);
    
    [ContextMenu("Testing ( Set to 75% Lerp )")]
    public void test_SetTo_75Pct() => fn_SetFillPct_Lerp(0.75f);

    [ContextMenu("Testing ( Set to 20% NoLerp )")]
    public void test_SetTo_20PctNoLerp() => fn_SetFillPct_NoLerp(0.20f);

    [ContextMenu("Testing ( Set to 80% NoLerp )")]
    public void test_SetTo_80PctNoLerp() => fn_SetFillPct_NoLerp(0.80f);

    #endregion
}
