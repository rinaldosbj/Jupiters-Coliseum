using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningLogic : MonoBehaviour
{
    private bool isMovingRight = true;
    [SerializeField] private float lightningSpeed = 0.01f;
    private float timer = 0f;
    [SerializeField] private float maxTime = 6f;
    private float directionTimer = 0f;
    [SerializeField] private float changeDirectionTime = 1f;


    void Update()
    {
        if (isMovingRight)
        {
            transform.position = new Vector2(transform.position.x + lightningSpeed, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - lightningSpeed, transform.position.y);
        }

        if (directionTimer < changeDirectionTime)
        {
            directionTimer += Time.deltaTime;
        }
        else
        {
            directionTimer = 0;
            ChangeDirection();
        }
        
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLife>().getHit(1);
        }
    }
}
