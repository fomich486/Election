using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    #region Singleton
    public static Settings Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public float SoundVolume;
    public float MusicVolume;
    public Sprite headImage;
    public AudioClip headSong;
    public AudioClip HitSong;
    public bool headSelection = false;
    public void PlaySound(string soundName)
    {
        var clip = Resources.Load(soundName) as AudioClip;
        FindObjectOfType<SoundControll>().PlaySound(clip);
    }
    
    
}
