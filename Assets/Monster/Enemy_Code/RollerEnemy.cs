using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerEnemy : MonoBehaviour
{

    public Transform player;  // 引用玩家的Transform，记得拖入Inspector
    public float shakeTriggerDistance = 8f;  // 抖动触发距离（玩家到怪物的距离）
    [Header("巡逻区间设置")]
    public float minX = 15f;
    public float maxX = 30f;

    [Header("移动设置")]
    public float rollSpeed = 5f;
    private Vector2 moveDirection = Vector2.right;

    [Header("攻击设置")]
    public int damage = 1;
    private bool isAttacking = false;
    private bool isRolling = false;

    [Header("Dust 特效")]
    public GameObject dustPrefab;
    private GameObject activeDust;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        moveDirection = Vector2.right;

        // 启动攻击循环
        animator.SetTrigger("Anticipation");
    }

    // 🔥【新加入的OnEnable方法，每次启用对象都会调用】
    void OnEnable()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        // 确保Animator不会有旧的Trigger干扰
        animator.ResetTrigger("Anticipation");
        animator.SetTrigger("Anticipation");

        // 状态重置
        isRolling = false;
        isAttacking = false;
    }

    void Update()
    {
        if (isRolling)
        {
            rb.velocity = moveDirection * rollSpeed;

            // 到边界就掉头
            if ((moveDirection.x > 0 && transform.position.x >= maxX) ||
                (moveDirection.x < 0 && transform.position.x <= minX))
            {
                FlipDirection();
                EndRollAttack(); // 自动触发收尾
            }
        }
    }

    void FlipDirection()
    {
        moveDirection.x *= -1;
        sr.flipX = !sr.flipX;

        // 同步 Dust flipX
        if (activeDust != null)
        {
            SpriteRenderer dustRenderer = activeDust.GetComponent<SpriteRenderer>();
            if (dustRenderer != null)
                dustRenderer.flipX = sr.flipX;
        }
    }

    // 🔥 动画事件触发
    public void StartRollAttack()
    {
        isRolling = true;
        isAttacking = true;

        SpawnDust();
        if(Vector2.Distance(transform.position, player.position) <= shakeTriggerDistance)
            CameraShake.Instance.ShakeCamera(0.5f, 0.2f);
    }

    public void EndRollAttack()
    {
        isRolling = false;
        isAttacking = false;
        rb.velocity = Vector2.zero;

        // 销毁 Dust
        if (activeDust != null)
        {
            Destroy(activeDust);
            activeDust = null;
        }

        // 再次进入 Anticipation，循环
        animator.SetTrigger("Anticipation");
        if(Vector2.Distance(transform.position, player.position) <= shakeTriggerDistance)
            CameraShake.Instance.ShakeCamera(0.2f, 0.05f);

    }

    void SpawnDust()
    {
        if (dustPrefab != null && activeDust == null)
        {
            // 🟡 调整这个偏移，让 Dust 生成在怪物脚边
            Vector3 spawnOffset = new Vector3(3f, -1f, 0f);
            if (sr.flipX)
                spawnOffset.x *= -1;

            Vector3 spawnPos = transform.position + spawnOffset;

            activeDust = Instantiate(dustPrefab, spawnPos, Quaternion.identity, transform);
            activeDust.tag = "Dust";

            SpriteRenderer dustRenderer = activeDust.GetComponent<SpriteRenderer>();
            if (dustRenderer != null)
            {
                dustRenderer.flipX = sr.flipX;
            }
        }
    }

    // 玩家碰撞检测
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
                player.TakeDamage(damage);
        }
    }
}
