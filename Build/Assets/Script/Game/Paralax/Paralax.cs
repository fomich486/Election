using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paralax : MonoBehaviour
{
    [SerializeField]
    float limits;
[SerializeField]
    RectTransform obj;
    [SerializeField]
    [Range(0f, 1f)]
    float speed;
    float direction = 0f;

    void Start()
    {
        //print(obj.anchoredPosition);
    }

    void Update()
    {
        direction =  GameController.Instance.myHead.GetDirection();
        //print(direction);
        if (direction > 0)
            if (obj.anchoredPosition.x >= -limits)
                obj.anchoredPosition = obj.anchoredPosition - Vector2.right * speed;
            else
                obj.anchoredPosition = new Vector2((limits - speed), obj.anchoredPosition.y);
        else if (direction < 0)
        {
            if (obj.anchoredPosition.x <= limits)
                obj.anchoredPosition = obj.anchoredPosition + Vector2.right * speed;
            else
                obj.anchoredPosition = new Vector2((-limits + speed), obj.anchoredPosition.y);
        }

    }
}
