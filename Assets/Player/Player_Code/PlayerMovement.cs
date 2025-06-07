using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    /* ======= 速度相关 ======= */
    public float walkSpeed = 5f;
    public float runSpeed  = 8f;
    private float currentSpeed;

    /* ======= 组件与状态 ======= */
    private Rigidbody2D rb;
    private Animator   animator;
    private Vector2    moveInput;
    private Vector2    lastMoveDirection;
    private bool       isRunning;

    /* ======= 脚步声设置 ======= */
    [Header("Footstep Sound")]
    public AudioClip footstepClip;
    public float footstepIntervalWalk = 0.45f;
    public float footstepIntervalRun  = 0.30f;

    [Range(0.5f, 1.5f)]
    public float footstepPitchMin = 0.9f;
    [Range(0.5f, 1.5f)]
    public float footstepPitchMax = 1.1f;

    /* ★NEW：声音大小滑杆（0=静音，1=原始音量） */
    [Range(0f, 1f)]
    public float footstepVolume = 0.6f;

    private AudioSource audioSource;
    private float       footstepTimer;
    /* ========================= */

    void Start()
    {
        rb       = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        /* 自动保证 AudioSource 存在 */
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        /* ===== 输入与动画 ===== */
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput   = moveInput.normalized;

        isRunning    = Input.GetKey(KeyCode.LeftShift);
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
        /* ====================== */

        HandleFootstepSound();      // 播放脚步声
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput != Vector2.zero ? moveInput * currentSpeed
                                                : Vector2.zero;
    }

    /* -------- 脚步声函数 -------- */
    private void HandleFootstepSound()
    {
        if (moveInput == Vector2.zero || footstepClip == null)
        {
            footstepTimer = 0f;
            return;
        }

        footstepTimer += Time.deltaTime;
        float interval = isRunning ? footstepIntervalRun : footstepIntervalWalk;

        if (footstepTimer >= interval)
        {
            audioSource.pitch = Random.Range(footstepPitchMin, footstepPitchMax);

            /* ★NEW：第二个参数是音量系数 */
            audioSource.PlayOneShot(footstepClip, footstepVolume);

            footstepTimer = 0f;
        }
    }
    /* -------------------------- */
}
