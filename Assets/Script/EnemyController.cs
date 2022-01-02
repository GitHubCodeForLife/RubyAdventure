using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool isVertical;
    Rigidbody2D rb2d;
    int direction = 1;
    float movingTimer;
    public float movingTime = 1.5f;

    private Animator animator;
    bool broken = true;

    ParticleSystem particleSystem;
    public AudioClip audioClip;
    public AudioClip audioFix;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        movingTimer = movingTime;
    }
    private void Update()
    {
        if (!broken)
        {
         
            return;
        } 
        movingTimer -= Time.deltaTime;
        if (movingTimer < 0)
        {
            direction *= -1;
            movingTimer = movingTime;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 pos = rb2d.position;
        if (isVertical)
        {
            pos.y += speed * Time.deltaTime *direction; 
        }
        else
        {
            pos.x += speed * Time.deltaTime *direction;
        }
        if (isVertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }else
        {
            animator.SetFloat("Move Y", 0);
            animator.SetFloat("Move X", direction);
        }
        rb2d.MovePosition(pos);
    }
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

    public void Fix()
    {
        PlaySound(audioFix);
        animator.SetTrigger("Fixed");
        broken = false;
        rb2d.simulated = false;
        particleSystem.Stop();
    }
    public void PlaySound(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
}
