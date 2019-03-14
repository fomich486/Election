using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MyHead : MonoBehaviour
{
    //public AudioClip choiceSong;
    public Rigidbody2D rb { get; private set; }
    float maxSpeed = 20f;
    float minSpeed = 10f;

    float timer = 0f;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();      
    }

    private void FixedUpdate()
    {
        // LooseSpeed();
        if (Mathf.Abs(rb.angularVelocity) > 450f)
        {
            rb.angularVelocity = UnityEngine.Random.Range (- 125f, -450f);
        }
        CheckSpeed();
    }

    private void CheckSpeed()
    {
        if (rb.velocity.x > 0 && rb.velocity.x < 10)
            rb.velocity = new Vector2(10, rb.velocity.y);
        else if (rb.velocity.x < 0)
        {
            timer += Time.fixedDeltaTime;
            if (timer > 3f)
            {
                GameController.Instance.GameOver.Invoke();
                Destroy(gameObject);
            }
        }
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
            PlayerController.Instance.changeDashAmount(1);
            Destroy(col.gameObject);
        }
        if (col.tag == "Spikes")
        {
            Settings.Instance.PlaySound("Spikes2");
            rb.simulated = false;
            rb.transform.SetParent(col.transform);
            GameController.Instance.GameOver?.Invoke();
        }
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

}
