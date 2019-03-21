using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if ((canControl))
        {
            InputControl();
            InputDash();
        }
    }
    [SerializeField]
    private int dashAvailable;

    private bool canControl = false;
    public bool CanControl { get { return canControl; } set { canControl = value; } }

    private bool headFly = false;

    public bool HeadFly;
    
    public int DashAvailable
    {
        get
        {
            return dashAvailable;
        }
        private set
        {
            dashAvailable = value;
        }
    }

    [SerializeField]
    private float hitForce = 5;

    
    public float dashForce = 5;


    [SerializeField]
    private float dashTimer = 3;
    private float nextDashTime;

    #region SingleTone
    public static PlayerController Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }
    #endregion


    void InputControl()
    {   
        if (!headFly)
        {

            Vector2 dir = GameController.Instance.ShootDirection.GetDirection();
            float forceModif = GameController.Instance.ForceModifire.GetForceModifire();
            Shooted(dir, forceModif);
        }
    }

    private void Shooted(Vector2 direction, float forceModifire)
    {
        GameController.Instance.myHead.GetImpulse(direction, hitForce * forceModifire );
        GameController.Instance.AfterShootAction.Invoke();
        headFly = !headFly;
        GameController.Instance.myHead.InstantiateTrail();
        nextDashTime = Time.time;
    }

    void InputDash()
    { 
        if (headFly)
        {
            if (( (Time.time > nextDashTime && dashAvailable > 0)))
            {
                GameController.Instance.myHead.GetImpulse(Vector2.down, dashForce);
                nextDashTime = Time.time + dashTimer;
                GameController.Instance.HUD.ChangeDash("-1", 0.5f);
                changeDashAmount(-1);
            }
        }
    }

    public void changeDashAmount(int amount)
    {
        DashAvailable += amount;
        
    }
}
