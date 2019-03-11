using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDirection : MonoBehaviour
{
    [SerializeField]
    private float _minAngle;
    [SerializeField]
    private float _maxAngleDiapasone;
    [SerializeField]
    private float _W;
    private float _startTime = 0f;
    private void OnEnable()
    {
        _startTime = Time.time;
    }
    private void Update()
    {
        Rotate();
    }

    public Vector2 GetDirection()
    {
        return transform.right;
    }

    private void Rotate()
    {
        transform.eulerAngles = new Vector3(0, 0, _minAngle + _maxAngleDiapasone * Mathf.Abs(Mathf.Sin((Time.time - _startTime) * _W)));
    }


}
