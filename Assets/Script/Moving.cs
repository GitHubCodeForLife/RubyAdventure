using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Transform destination;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubyController =  collision.GetComponent<RubyController>();

        if (rubyController != null)
            rubyController.SetDestination(destination.position);
    }

}
