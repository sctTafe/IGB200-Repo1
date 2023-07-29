using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ScottBarley.IGB200.V1
{
    public class UI_BasicTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timer_TMP;
        [SerializeField] private float _time;
        [SerializeField] private bool _timerIsOn;

        private float _timeLeft;


        private void Start()
        {
            _timeLeft = _time;
        }

        void Update()
        {
            if (_timerIsOn)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    UpdateTimer(_timeLeft);
                }
                else
                {
                    Debug.Log("Time Up!");
                    _timerIsOn = false;
                    _timeLeft = 0;
                }
            }
        }

        void UpdateTimer(float currentTime)
        {
            currentTime += 1;
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            _timer_TMP.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }
}