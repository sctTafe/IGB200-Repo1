using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SliderInput_UI : MonoBehaviour
{
    public UnityEvent<float> _OnSiderChangeEventRelay;
    public void OnSliderValueChanged(float value) {
        Debug.Log("Slider value changed: " + value);
        _OnSiderChangeEventRelay?.Invoke(value);
    }
}


