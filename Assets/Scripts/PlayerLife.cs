using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int life = 1;
    private float timer = 0f;
    [SerializeField] private float cooldownTime = 1f;
    private bool canGetHit = true;
    private SpriteRenderer sprite;
    [SerializeField] private Text lifeText;

    private void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        lifeText.text = life.ToString();
    }

    private void Update()
    {
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

    public void getHit(int amount)
    {
        if (canGetHit)
        {
            life -= amount;
            canGetHit = false;
            lifeText.text = life.ToString();
            if (life <= 0)
            {
                Die();
            }
            
        }
    }

    private void Die()
    {
        lifeText.text = "YOU DIED";
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Enemy"))
        {
            getHit(1);
        }
    }
}
