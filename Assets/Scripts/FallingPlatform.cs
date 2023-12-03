using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float platformSpeed = 0.03f;
    [SerializeField] private float maxPosition = 20f;
    [SerializeField] private bool mustStop = true;
    [SerializeField] private float stopPosition = 10f;


    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - platformSpeed);

        if (mustStop)
        {
            if (transform.position.y <= stopPosition)
            {
                Destroy(gameObject.GetComponent<FallingPlatform>());
            }
        }
        else
        {
            if (transform.position.y <= -maxPosition)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
