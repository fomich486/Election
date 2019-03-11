using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birdy : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float changeDirectionTime = 3;
    private float nextDirectionUpdate;

    Rigidbody2D rb;

    [SerializeField]
    private float carryTime = 3;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextDirectionUpdate = Time.time;
    }
    void Update()
    {

        Vector2 velocity = rb.velocity;
        velocity.x = speed * transform.localScale.x * -1;
        rb.velocity = velocity;

        if (Time.time > nextDirectionUpdate)
        {
            ChangeDirection();
            nextDirectionUpdate = Time.time + changeDirectionTime;
        }

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        MyHead head = collider.gameObject.GetComponent<MyHead>();
        if (head != null)
        {
            Settings.Instance.PlaySound("Bird");
            StartCoroutine (HeadCarrying(head));
        }
    }

    IEnumerator HeadCarrying(MyHead head)
    {
        head.TakeHead(0);
        head.transform.SetParent(transform);
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(carryTime);

        transform.DetachChildren();
        head.TakeHead(rb.velocity.x);

        yield return new WaitForSeconds(1);
        GetComponent<CircleCollider2D>().enabled = true;

    }

    private void ChangeDirection()
    {
        
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); ;
        }
        else
        {
          transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
