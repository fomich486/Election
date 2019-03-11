using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MovingDirection {X, Y,NoMotion}
    public MovingDirection direction;

    [SerializeField]
    private float w;
    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float fi0=0;

    Vector3 startPosition;
    Rigidbody2D rb;
    private float _startTime;
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == MovingDirection.Y)
            rb.velocity = new Vector3(0, amplitude * Mathf.Cos(w * (Time.time - _startTime) + fi0));
        else if (direction == MovingDirection.X)
            rb.velocity = new Vector3(amplitude * Mathf.Cos(w * (Time.time - _startTime) + fi0), 0);
        else if (direction == MovingDirection.NoMotion)
        {
            return;
        }
    }
}
