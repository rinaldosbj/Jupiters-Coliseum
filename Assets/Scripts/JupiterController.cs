using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class JupiterController : MonoBehaviour
{
    [SerializeField] private int life = 1;
    private float timer = 0f;
    [SerializeField] private float cooldownTime = 1f;
    private bool canGetHit = true;
    public bool jupiterIsAlive = true;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private GameObject player;
    private SwarmLogic swarmLogic;

    private void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        swarmLogic = GetComponentInParent<SwarmLogic>();
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
                SpriteRenderer sprite = swarmLogic.sprite;
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

            SpriteRenderer sprite = swarmLogic.sprite;
            sprite.color = Color.red;

            canGetHit = false;

            if (life == 0)
            {
                SwarmLogic swarmLogic = FindObjectOfType<SwarmLogic>();
                swarmLogic.FallingPlatforms();
                Debug.Log("murio");
                swarmLogic.jupiterDied();
                swarmLogic.sprite.enabled = false;
            }
        }
    }

    public void jupiterRevive()
        {
            Debug.Log("hehehe");
            life = 1;
            jupiterIsAlive = true;
            swarmLogic.sprite.enabled = true;
        }
}
