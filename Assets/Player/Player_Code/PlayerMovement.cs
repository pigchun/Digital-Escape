using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    /* ======= 速度相关 ======= */
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private float currentSpeed;

    /* ======= 组件与状态 ======= */
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private bool isRunning;

    /* ======= 脚步声设置 ======= */
    [Header("Footstep Sound")]
    public AudioClip footstepClip;
    public float footstepIntervalWalk = 0.45f;
    public float footstepIntervalRun = 0.30f;

    [Range(0.5f, 1.5f)]
    public float footstepPitchMin = 0.9f;
    [Range(0.5f, 1.5f)]
    public float footstepPitchMax = 1.1f;

    /* ★NEW：声音大小滑杆（0=静音，1=原始音量） */
    [Range(0f, 1f)]
    public float footstepVolume = 0.6f;

    private AudioSource audioSource;
    private float footstepTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        /* 自动保证 AudioSource 存在 */
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        /* ========== 移动相关 ========== */
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

        /* ========== 射击相关 ========== */
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mouseWorldPos - (Vector2)transform.position).normalized;

        if (Input.GetMouseButton(0)) // 鼠标左键按住
        {
            animator.SetBool("Shooting", true);
            animator.SetFloat("ShootingX", shootDir.x);
            animator.SetFloat("ShootingY", shootDir.y);
        }
        else
        {
            animator.SetBool("Shooting", false);
        }

        /* ========== 脚步声播放 ========== */
        HandleFootstepSound();
    }

    void FixedUpdate()
    {
        // 获取当前是否处于射击状态
        bool isShooting = animator.GetBool("Shooting");

        if (!isShooting)
        {
            rb.velocity = moveInput != Vector2.zero ? moveInput * currentSpeed
                                                    : Vector2.zero;
        }
        else
        {
            rb.velocity = Vector2.zero; // 禁止移动
        }
    }


    /* -------- 脚步声函数 -------- */
    
    private void HandleFootstepSound()
    {
        // 🔒 如果在射击状态，不播放脚步声
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

    /* -------------------------- */
}
