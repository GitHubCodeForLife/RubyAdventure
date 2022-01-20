using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Fix();
                RubyController rubyController = FindObjectOfType<RubyController>();
                if (rubyController != null)
                    rubyController.FixSuccuess();
            }
            Debug.Log("Bullet - hit " + collision.name);
            Destroy(gameObject);
        }
    }
}
