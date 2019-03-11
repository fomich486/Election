using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotation : MonoBehaviour
{
    [SerializeField]
    private float anglePerUpdate;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * anglePerUpdate * Time.fixedDeltaTime);
    }
}
