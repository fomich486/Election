using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MyHead : MonoBehaviour
{

    //public AudioClip choiceSong;
    public ParticleSystem _trail;
    public Rigidbody2D rb { get; private set; }
    float maxSpeed = 20f;
    float minSpeed = 10f;
    public float LastVelocityValue = 0;
    float timer = 4f;
    bool isPlayingClock = true;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();            
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.angularVelocity) > 450f)
        {
            rb.angularVelocity = UnityEngine.Random.Range (- 125f, -450f);
        }
        CheckSpeed();
        GameController.Instance.HUD.LoseTimer.text = ((int)timer).ToString();
    }

    //private void LateUpdate()
    //{
    //    if (_lastVelocityValue * rb.velocity.y < 0)
    //    {
    //        GameController.Instance.StartCameraSetup.YDirectonCameraFollow(rb.velocity.y);
    //        GameController.Instance.StartCameraSetup.StartCoroutine(GameController.Instance.StartCameraSetup.YDirectonFlagFolow(rb.velocity.y));
    //    }
    //    _lastVelocityValue = rb.velocity.y;
    //}

    private void CheckSpeed()
    {
        if (rb.velocity.x > 0 && rb.velocity.x < 10)
        {
            timer = 4f;
            GameController.Instance.HUD.LoseWarning.gameObject.SetActive(false);
            rb.velocity = new Vector2(10, rb.velocity.y);
        }
        if (rb.velocity.x > 0)
            GameController.Instance.HUD.LoseWarning.gameObject.SetActive(false);
        else if (rb.velocity.x < 0)
        {
            ClockTicking();
            GameController.Instance.HUD.LoseWarning.gameObject.SetActive(true);          
            timer -= Time.fixedDeltaTime;
            if (timer < 1f)
            {
                Settings.Instance.PlaySound("GoingBack");
                GameController.Instance.GameOver.Invoke();
                GameController.Instance.HUD.SorryText.text = "Ваш кандидат був знятий з президентської гонки.";
                Destroy(gameObject);
            }
        }
    }

    void ClockTicking()
    {
        if (isPlayingClock)
        {
            Settings.Instance.PlaySound("ClockTicking");
            StartCoroutine(Tick());
        }
    }
    IEnumerator Tick()
    {
        isPlayingClock = false;
        yield return new WaitForSeconds(0.82f);
        isPlayingClock = true;
    }

        public void InstantiateTrail()
    {
        _trail.gameObject.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D col)
      {
        if (col.tag == "Portal1")
        {
            col.GetComponentInParent<Portals>().TranslateToPortal2();
            Settings.Instance.PlaySound("EUportal");
        }
        if (col.tag == "Portal2")
        {
            col.GetComponentInParent<Portals>().TranslateToPortal1();
            Settings.Instance.PlaySound("NATOportal");
        }
        if (col.tag == "Bomb")
        {
            Settings.Instance.PlaySound("GrechkaSound");
            OnBombEnter(col.GetComponent<Bomb>().explosionForce);
        }
        if (col.tag == "Coin")
        {
            Settings.Instance.PlaySound("Coin");
            GameController.Instance.HUD.ChangeDash("+1", 1.3f);
            PlayerController.Instance.changeDashAmount(1);
            Destroy(col.gameObject);
        }
        if (col.tag == "Spikes")
        {
            Settings.Instance.PlaySound("Spikes");
            GameController.Instance.HUD.SorryText.text = "Вила Ляшка не щадять нікого.";
            GameController.Instance.HUD.LoseWarning.gameObject.SetActive(false);
            rb.simulated = false;
            rb.transform.SetParent(col.transform);
            GameController.Instance.GameOver?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {    
           
            Settings.Instance.PlaySound(Settings.Instance.HitSong.name);
        
    }

    void OnBombEnter(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        GetImpulse(Vector2.up, force);
    }

    public void TakeHead(float birdSpeed)
    {
        rb.simulated = !rb.simulated;
        if (rb.simulated)
            rb.velocity = new Vector2(birdSpeed + rb.velocity.x * (birdSpeed / Mathf.Abs(birdSpeed)), 0f);

    }

    public void setNewPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
        

    public void GetImpulse(Vector2 direction, float force)
    {
        if (rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x,0f);
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public string CalculateScore()
    {
        float result = (Mathf.Round((transform.position.x * 10f)) / 10000f)*100f;
        return result.ToString();
    }

    public float PositionX()
    {
        return transform.position.x;
    }

    public void SimulationHead(bool state)
    {
        rb.simulated = state;
    }

    public float GetDirection()
    {
        if (rb.velocity.x != 0f)
            return rb.velocity.x / Math.Abs(rb.velocity.x);
        else
            return 0f;
    }

    public float GetYDirection()
    {
        return rb.velocity.y;
    }
}
