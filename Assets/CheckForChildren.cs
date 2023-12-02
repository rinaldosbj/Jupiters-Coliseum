using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForChildren : MonoBehaviour
{
    void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            // Destroy if has no child
            Destroy(gameObject);
        }
    }
}
