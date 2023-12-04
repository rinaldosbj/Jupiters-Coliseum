using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life = 2;
    private float timer = 0f;
    [SerializeField] private float cooldownTime = 1f;
    private bool canGetHit = true;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private GameObject player;

    private void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        rb.position = Vector3.MoveTowards(transform.position,player.transform.position,0.05f);
        if (!canGetHit)
        {
            sprite.color = Color.red;
            if (timer < cooldownTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                canGetHit = true;
            }
        }
        else 
        {
            sprite.color = Color.white;
        }
    }

    public void getHit()
    {
        if (canGetHit)
        {
            life --;
            canGetHit = false;
            if (life == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
