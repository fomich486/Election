using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rb;


    [SerializeField]
    public float explosionForce;

    [SerializeField]
    private ParticleSystem particleBomb;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MyHead>() != null)
        {
            Instantiate(particleBomb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    


}
