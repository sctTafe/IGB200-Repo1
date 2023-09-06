//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ProgressBar_UI : MonoBehaviour
//{
//    public Image _fillBar_Img;
//    public float _barUilerpSpeed;
//    public float _startValue = 1000;


//    private float _currentValue, _maxValue = 1000;
    
//    private void Start() {
//        _currentValue = _startValue;
//    }

//    private void Update() {
//        if (_currentValue > _maxValue) _currentValue = _maxValue;

//        _barUilerpSpeed = 3f * Time.deltaTime;


//        BarFiller();
//    }

//    void BarFiller() {
//        _fillBar_Img.fillAmount = Mathf.Lerp(_fillBar_Img.fillAmount, _currentValue / _maxValue, _barUilerpSpeed);
//    }

//    public void fn_SetValueFull_Pct(float health_pct) {
//        _currentValue = health_pct * _maxValue;
//    }



//    private float _delay = 10;

//    public void FixedUpdate() {
//        // TEST_LowerHealth();
//    }

//    private void TEST_LowerHealth() {
//        if (_delay > 0) {
//            _delay -= Time.fixedDeltaTime;

//            if (_delay <= 0) {
//                _currentValue = _currentValue - 100;
//                _delay = 10;
//            }
//        }
//    }



//}
