/*
Simple Sound Manager (c) 2016 Digital Ruby, LLC
http://www.digitalruby.com

Source code may no longer be redistributed in source format. Using this in apps and games is fine.
*/

using UnityEngine;
using UnityEngine.UI;

using System.Collections;

// Be sure to add this using statement to your scripts
// using DigitalRuby.SoundManagerNamespace

namespace DigitalRuby.SoundManagerNamespace
{
    public class BattleAudioManager : MonoBehaviour
    {
        public AudioSource[] SoundAudioSources;
        public AudioSource[] MusicAudioSources;

        public void PlaySound(int index)
        {
            SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
        }
    }
}