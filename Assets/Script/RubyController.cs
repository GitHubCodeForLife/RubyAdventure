using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// MSSV: 18120629 
/// Ho va Ten: Tran Van Tu
/// Hieu ung shake screen
/// Map rong
/// </summary>

public class RubyController : MonoBehaviour
{
    private int enemiesCount;

    public GameObject effectPrefabs;
    public GameObject loseMenu;
    //Input Action asset
    public PlayerInputActions inputActions;
    Vector2 currentInput;
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

        //Initial Player Input Action
        inputActions = new PlayerInputActions();
        //Register Event
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += OnMovement;
        inputActions.Player.Movement.canceled += OnMovement;
        inputActions.Player.Launch.performed += OnLaunch;
        inputActions.Player.Talk.performed += Talk; 
    }

    private void Start()
    {
        if (PlayerInfo.IsContinue == true)
            LoadGame();
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        Debug.Log("Health: " + this.currentHealth);
    }
    private void LoadGame()
    {
        //Load Game with Prefabs
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.Load(1);
        Vector2 pos;
        if (playerInfo.X != 0)
            pos = new Vector2(playerInfo.X, playerInfo.Y);
        else
            pos = transform.position;
        if (playerInfo.CurrentHealth != 0)
            this.currentHealth = playerInfo.CurrentHealth;
        transform.position = pos;

        //Load Game with file
        if (GameStorageManager.gameResource != null)
        {
            enemiesCount = GameStorageManager.gameResource.enemiesCount;
            UIHealthBar.instance.SetEnemy(enemiesCount);
            Debug.Log("Enemies count: " + GameStorageManager.gameResource.enemiesCount);
        }
    }
    private void Talk(InputAction.CallbackContext obj)
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

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
            currentInput = context.ReadValue<Vector2>();
        else if (context.canceled)
            currentInput = Vector2.zero;
        Move();

    }

    private void Move()
    {
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        horizontal = currentInput.x;
        vertical = currentInput.y;
    }

    public void OnLaunch(InputAction.CallbackContext context)
    {
        Debug.Log("Launch");
        Launch();
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
        //Move
        if (currentInput.magnitude > 0.01)
        {
            Vector2 position = transform.position;
            position.x += horizontal * speed * Time.deltaTime;
            position.y += speed * vertical * Time.deltaTime;
            rb2d.MovePosition(position);
        }
        //Animation
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (invicibleTimer.isEnd == false)
                return;
            invicibleTimer.Reset();
            animator.SetTrigger("Hit");
            CameraShake cameraShake =  GameObject.FindObjectOfType<CameraShake>();
            if (cameraShake != null)
                cameraShake.shakeDuration = 1f;
            Vector3 test = new Vector2(0,1f);
            GameObject effect = GameObject.Instantiate(effectPrefabs, transform.position + test, transform.rotation);
            Destroy(effect, 1f);
        }

        this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth /(float) maxHealth);
        if (currentHealth == 0)
        {
            Debug.Log("Lose Game");
            Time.timeScale = 0f;
            loseMenu.SetActive(true);
        }
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

    public void SaveGame()
    {
        Debug.Log("Save Game");
        //Save game with Prefabs
        PlayerInfo playerInfo = new PlayerInfo();
        Vector2 pos = transform.position;
        playerInfo.X = pos.x;
        playerInfo.Y = pos.y;
        playerInfo.CurrentHealth = this.currentHealth;
        playerInfo.Save(1);

        //Save Game with file
        GameResource gameResource = new GameResource();
        gameResource.enemiesCount = enemiesCount;
        GameStorageManager.gameResource = gameResource;
        GameStorageManager.Save();
    }

    public void FixSuccuess()
    {
        enemiesCount++;
        UIHealthBar.instance.SetEnemy(enemiesCount);
    }
}
