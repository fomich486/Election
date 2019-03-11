using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    public AudioSource AS_Music;
    public AudioSource AS_Sound;
    public Slider sl_Music;
    public Slider sl_Sound;

    private void Start()
    { 
        AS_Music.volume = Settings.Instance.MusicVolume;
        sl_Music.value = Settings.Instance.MusicVolume;
        AS_Sound.volume = Settings.Instance.SoundVolume;
        sl_Sound.value = Settings.Instance.SoundVolume;
    }

    public void OnMusicChange()
    {
        AS_Music.volume = sl_Music.value;
        Settings.Instance.MusicVolume = sl_Music.value;
    }
    public void OnSoundChange()
    {
        AS_Sound.volume = sl_Sound.value;
        Settings.Instance.SoundVolume = sl_Sound.value;
    }

    public void PlaySound(AudioClip clip)
    {
        AS_Sound.PlayOneShot(clip);
    }
    public void StartMusic()
    {
        AS_Music.enabled = true;
    }
}
