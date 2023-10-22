using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCompletionCelebration_Mng : MonoBehaviour
{

    // - UI Panel -
    [SerializeField] private GameObject _winUIPanel;
    [SerializeField] private GameObject _loseUIPanel;
    // - Audio - 
    [SerializeField] private AudioSource _winAudioSource;
    [SerializeField] private AudioSource _loseAudioSource;

    public void Start()
    {
        _winUIPanel.SetActive(false);
        _loseUIPanel.SetActive(false);
    }

    public void fn_BattleEndTrigger()
    {
        if (StaticData.isBattleWon)
        {
            _winUIPanel.SetActive(true);
            _winAudioSource.PlayOneShot(_winAudioSource.clip);

        }
        else
        {
            _loseUIPanel.SetActive(true);
            _loseAudioSource.PlayOneShot(_loseAudioSource.clip);
        }
    }


}
