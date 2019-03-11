using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForceModifire : MonoBehaviour
{
    [SerializeField]
    private Slider _forceSlider;
    [SerializeField]
    private float _W;
    private float _startTime = 0f;
    private void OnEnable()
    {
        _startTime = Time.time;
    }
    public float GetForceModifire()
    {
        return _forceSlider.value;
    }

    private void Update()
    {
        _forceSlider.value = _forceSlider.minValue + (_forceSlider.maxValue - _forceSlider.minValue) * Mathf.Abs(Mathf.Cos(_W * (Time.time-_startTime)  + Mathf.PI/4));
    }
}
