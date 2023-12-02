using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private SpriteRenderer sprite;
    new private BoxCollider2D collider;
    private float timer = 0f;
    [SerializeField] private float cooldownTime = 1f;
    private bool canHit = true;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        sprite.enabled = false;
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canHit)
        {
            if (timer < cooldownTime)
            {
                timer += Time.deltaTime;
                sprite.enabled = false;
                collider.enabled = false;
            }
            else
            {
                timer = 0;
                canHit = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && canHit)
        {
            sprite.enabled = true;
            collider.enabled = true;
            nowCanHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyLife>().getHit();
        }
    }

    async void nowCanHit()
    {
        await Task.Delay(500);
        canHit = false;
    }
}
