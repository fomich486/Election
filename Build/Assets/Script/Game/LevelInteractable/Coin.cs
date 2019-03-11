using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem coinParticle;
    private void Awake()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MyHead>() != null)
        {
            PlayerController.Instance.changeDashAmount(1);
            Destroy(gameObject);
        }
    }
    public void ParticleSpawn()
    {
        coinParticle.Play();
    }
}
