using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> levelBasics;

    
    private float groundLength = 216.2902f;

    Vector3 nextInstantiatePosition = Vector3.zero;
    float nextSpawnTriggerX;
    private void Awake()
    {
        Spawn(0);
    }

    private void Update()
    {
        if (GameController.Instance.myHead.PositionX() > nextSpawnTriggerX)
            Spawn(Random.Range(1, levelBasics.Count)); //Here spawn from one

    }

    void Spawn(int number)
    {
        Instantiate(levelBasics[number], nextInstantiatePosition, Quaternion.identity);
        nextInstantiatePosition += Vector3.right * groundLength;
        nextSpawnTriggerX = nextInstantiatePosition.x - groundLength;
    }
}
