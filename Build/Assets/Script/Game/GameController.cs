using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public UnityEvent UIUpdate;
    public UnityEvent GameStarts;
    public UnityEvent GameOver;
    public UnityEvent ActivatePlayerControl;
    public UnityEvent AfterShootAction;

    public MyHead myHead;
    public ShootDirection ShootDirection;
    public ForceModifire ForceModifire;
    public StartCameraSetup StartCameraSetup;
    public SoundControll soundControll;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }

    }
    void Start()
    {
        GameStarts.AddListener(StartSettings);
        ActivatePlayerControl.AddListener( EnableControl);
        AfterShootAction.AddListener(ShootComplited);
        if(UIUpdate!=null)
            UIUpdate.Invoke();
        GameStarts?.Invoke();
    }
    void Update()
    {
        if (UIUpdate != null)
            UIUpdate.Invoke();
    }
    private void EnableControl()
    {
        ShootDirection.gameObject.SetActive(true);
        ForceModifire.gameObject.SetActive(true);
        PlayerController.Instance.CanControl = true;
    }
    private void ShootComplited()
    {
        soundControll.StartMusic();
        Destroy(ShootDirection.gameObject);
        Destroy(ForceModifire.gameObject);

    }
    private void StartSettings()
    {
        myHead.GetComponent<SpriteRenderer>().sprite = Settings.Instance.headImage;
    }
}
