using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private SpriteRenderer sprite;
    new private BoxCollider2D collider;
    private float timer = 0f;
    [SerializeField] private float cooldownTime = 0.1f;
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
        
        if (Input.GetKeyDown(KeyCode.Z) && !Input.GetKey(KeyCode.W) && canHit) {
            regularAttack();
        }
        
        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Z) && canHit) {
            upAttack();
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Z) && canHit) {
            downAttack();
        }
        
    }

    void upAttack()
    {
        sprite.enabled = true;
        collider.enabled = true;

        // Calculate the rotation for the upward attack weapon (to point horizontally)
        float rotationAngle = transform.localScale.x > 0 ? 90f : -90f; // Adjust the angle according to player's direction

        // Set the rotation for the upward attack weapon
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

        // Set the position for the upward attack weapon
        transform.localPosition = new Vector3(0.5f, 1.5f, 0f); // Adjust the position if needed

        nowCanHit();
        Debug.Log("Up attack");
    }

    void downAttack()
    {
        sprite.enabled = true;
        collider.enabled = true;

        // Calculate the rotation for the downward attack weapon (to point horizontally)
        float rotationAngle = transform.localScale.x > 0 ? -90f : 90f; // Adjust the angle according to player's direction

        // Set the rotation for the downward attack weapon
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

        // Set the position for the downward attack weapon (below the player)
        transform.localPosition = new Vector3(0f, -1.5f, 0f); // Adjust the position if needed

        nowCanHit();
        Debug.Log("Down attack");
    }

    void regularAttack()
    {
        sprite.enabled = true;
        collider.enabled = true;

        // Reset the rotation for the regular attack weapon
        transform.localRotation = Quaternion.identity;

        // Reset the position for the regular attack weapon
        transform.localPosition = new Vector3(1f, 0.5f, 0f); // Adjust the position if needed

        nowCanHit();
        Debug.Log("Regular attack");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Aguia"))
        {
            other.GetComponent<EnemyLife>().getHit();
        } else if(other.CompareTag("Jupiter")) {
            other.GetComponent<JupiterController>().getHit();
        }
    }

    async void nowCanHit()
    {
        await Task.Delay(50);
        canHit = false;
    }
}
