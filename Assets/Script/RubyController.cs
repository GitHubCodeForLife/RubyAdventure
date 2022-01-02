using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed;
    float horizontal;
    float vertical;
    private Rigidbody2D rb2d;

    public int maxHealth = 5;
    int currentHealth;

    Timer invicibleTimer;

    private Animator animator;

    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject bulletPrefab;
    public float force = 300f;
    private AudioSource audioSource;
    public AudioClip audioLaunch;

    private void Awake()
    {
        currentHealth = 5;
        audioSource = gameObject.GetComponent<AudioSource>();
        invicibleTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        invicibleTimer.maxTime = 3;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log("Ruby - Ray cast" + hit.collider.name);
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }

    private void Launch()
    {
        PlaySound(audioLaunch);
        animator.SetTrigger("Launch");
        GameObject bulletObject = Instantiate(bulletPrefab, rb2d.position, Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        if (bullet != null) { 
            bullet.Launch(lookDirection, 300f); 
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;
        rb2d.MovePosition(position);
        // transform.position = position;
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (invicibleTimer.isEnd == false)
                return;
            invicibleTimer.Reset();
            animator.SetTrigger("Hit");
        }

        this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth /(float) maxHealth);
    }
    public void PlaySound(AudioClip audioClip)
    {
        //audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
    public void SetDestination(Vector2 position)
    {
        Debug.Log("Move to position " + position);
        //rb2d.MovePosition(position);
        transform.position = position;
    }
}
