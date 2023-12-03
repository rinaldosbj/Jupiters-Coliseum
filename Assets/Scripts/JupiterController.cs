using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class JupiterController : MonoBehaviour
{
    [SerializeField] private int life = 3;
    private float timer = 0f;
    [SerializeField] private float cooldownTime = 1f;
    private bool canGetHit = true;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private GameObject player;
    private SwarmLogic jupiter;

    private void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        jupiter = GetComponentInParent<SwarmLogic>();
    }

    private void Update()
    {
        if (!canGetHit)
        {
            if (timer < cooldownTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpriteRenderer sprite = jupiter.sprite;
                sprite.color = Color.white;
                timer = 0;
                canGetHit = true;
            }
        }
    }

    public void getHit()
    {
        if (canGetHit)
        {
            Debug.Log("levo");
            life --;

            SpriteRenderer sprite = jupiter.sprite;
            sprite.color = Color.red;

            canGetHit = false;

            if (life == 0)
            {
                jupiter.FallingPlatforms();
                Debug.Log("murio");
                Destroy(gameObject);
                Destroy(jupiter.gameObject);
            }
        }
    }
}
