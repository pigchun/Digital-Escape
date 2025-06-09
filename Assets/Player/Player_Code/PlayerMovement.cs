using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    // ====== 子弹与射击 ======
    [Header("Shooting Settings")]
    public GameObject m7Spike;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float shootCooldown = 0.2f;

    private float lastShootTime = 0f;

    [Header("Shooting Sound")]
    public AudioClip shootClip;
    [Range(0f, 1f)] public float shootVolume = 0.7f;
    public float clipPlayDuration = 0.3f;

    private Coroutine soundStopCoroutine;


    // ====== 移动速度设置 ======
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private float currentSpeed;


    // ====== 脚步声设置 ======
    [Header("Footstep Sound")]
    public AudioClip footstepClip;
    public float footstepIntervalWalk = 0.45f;
    public float footstepIntervalRun = 0.30f;

    [Range(0.5f, 1.5f)] public float footstepPitchMin = 0.9f;
    [Range(0.5f, 1.5f)] public float footstepPitchMax = 1.1f;
    [Range(0f, 1f)] public float footstepVolume = 0.6f;

    private float footstepTimer;


    // ====== 状态与组件 ======
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private bool isRunning;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 保证 AudioSource 存在
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }


    void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
        HandleFootstepSound();
    }

    void FixedUpdate()
    {
        bool isShooting = animator.GetBool("Shooting");

        if (!isShooting)
        {
            rb.velocity = moveInput != Vector2.zero ? moveInput * currentSpeed : Vector2.zero;
        }
        else
        {
            rb.velocity = Vector2.zero; // 射击时禁止移动
        }
    }


    // ====== 移动逻辑 ======
    private void HandleMovementInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveInput != Vector2.zero)
            lastMoveDirection = moveInput;

        animator.SetFloat("WalkX", moveInput.x);
        animator.SetFloat("WalkY", moveInput.y);

        if (isRunning)
        {
            animator.SetFloat("RunX", moveInput.x);
            animator.SetFloat("RunY", moveInput.y);
        }

        animator.SetFloat("IdleX", lastMoveDirection.x);
        animator.SetFloat("IdleY", lastMoveDirection.y);

        animator.SetBool("isWalking", moveInput != Vector2.zero);
        animator.SetBool("isRunning", isRunning && moveInput != Vector2.zero);
    }


    // ====== 射击逻辑 ======
    private void HandleShootingInput()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mouseWorldPos - (Vector2)transform.position).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            PlayShootSound(); // 播放音效（从头）
        }

        if (Input.GetMouseButton(0))
        {
            // 动画持续播放
            animator.SetBool("Shooting", true);
            animator.SetFloat("ShootingX", shootDir.x);
            animator.SetFloat("ShootingY", shootDir.y);

            // ✅ 持续发射（根据 cooldown 限制）
            if (Time.time - lastShootTime >= shootCooldown)
            {
                Shoot(shootDir);
                lastShootTime = Time.time;
            }
        }
        else
        {
            // 松开时停止动画与音效
            animator.SetBool("Shooting", false);
            audioSource.Stop();
        }
    }


    private void Shoot(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(m7Spike, firePoint.position, Quaternion.Euler(0f, 0f, angle));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * bulletSpeed;
    }

    private void PlayShootSound()
    {
        if (shootClip == null) return;

        audioSource.clip = shootClip;
        audioSource.volume = shootVolume;
        audioSource.time = 0f;
        audioSource.Play();

        if (soundStopCoroutine != null)
            StopCoroutine(soundStopCoroutine);

        soundStopCoroutine = StartCoroutine(StopAudioAfterSeconds(clipPlayDuration));
    }

    private IEnumerator StopAudioAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // 如果玩家没持续按住鼠标，强制停止
        if (!Input.GetMouseButton(0))
            audioSource.Stop();
    }


    // ====== 脚步声 ======
    private void HandleFootstepSound()
    {
        if (animator.GetBool("Shooting") || moveInput == Vector2.zero || footstepClip == null)
        {
            footstepTimer = 0f;
            return;
        }

        footstepTimer += Time.deltaTime;
        float interval = isRunning ? footstepIntervalRun : footstepIntervalWalk;

        if (footstepTimer >= interval)
        {
            audioSource.pitch = Random.Range(footstepPitchMin, footstepPitchMax);
            audioSource.PlayOneShot(footstepClip, footstepVolume);
            footstepTimer = 0f;
        }
    }
}
