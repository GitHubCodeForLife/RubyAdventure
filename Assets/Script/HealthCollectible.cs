using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip audioClip;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Health Collectible " + collision.name);
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController != null)
        {
            rubyController.ChangeHealth(1);
            rubyController.PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
