using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
            rubyController.PlaySound(audioClip);
           // Destroy(gameObject);
        }
    }
}