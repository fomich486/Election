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
    public void PlaySound(string soundName)
    {
        var clip = Resources.Load(soundName) as AudioClip;
        FindObjectOfType<SoundControll>().PlaySound(clip);
    }
    
    public void Kostyl()
    {
        StartCoroutine(KostylCoroutine());
        
    }
    IEnumerator KostylCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        //GameObject.Find("Start_btn").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        GameObject.FindGameObjectWithTag("StartBtn").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }
    
}
