using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // 12 ou -12
    [SerializeField] private bool isMovingRight = true;
    [SerializeField] private float ballSpeed = 0.1f;
    [SerializeField] private float maxPosition = 12f;

    void Update()
    {
        if (isMovingRight)
        {
            transform.position = new Vector2(transform.position.x + ballSpeed, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - ballSpeed, transform.position.y);
        }
        
        if (transform.position.x <= -maxPosition || transform.position.x >= maxPosition)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
