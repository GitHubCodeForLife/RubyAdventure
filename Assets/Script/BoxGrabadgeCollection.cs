using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrabadgeCollection : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Box Garbadge " +collision.name);
        Destroy(collision);
    }
}
