using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    private CircleCollider2D[] colliders;

    private float enableColliderTime;

    [SerializeField]
    private float enableDelay = 3f;

    [SerializeField]
    private Transform portal1;

    [SerializeField]
    private Transform portal2;
    // Start is called before the first frame update
    void Start()
    {
        
        colliders = GetComponentsInChildren<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!colliders[0].enabled && Time.time > enableColliderTime)
            foreach (CircleCollider2D bc in colliders)
            {
                bc.enabled = true;
            }    
    }

    public void TranslateToPortal2()
    {
        GameController.Instance.myHead.setNewPosition(portal2.position);
        RemoveColliders();
    }

    public void TranslateToPortal1()
    {
        GameController.Instance.myHead.setNewPosition(portal1.position);
        RemoveColliders();
    }

    void RemoveColliders()
    {
        
        foreach (CircleCollider2D bc in colliders)
        {
            bc.enabled = false;
        }
        enableColliderTime = Time.time + enableDelay;
    }

}
