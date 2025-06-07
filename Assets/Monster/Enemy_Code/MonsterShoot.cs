using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Attacking,
    Cooldown
}

[RequireComponent(typeof(Collider2D))]
public class MonsterShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float cooldownTime = 2f;
    public Animator animator;

    public AudioClip shootClip;        // 拖一个音效进来
    public AudioSource audioSource;    // 怪物或空物体上的 AudioSource

    [Header("Pitch Range")]
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;

    private bool playerInRange = false;
    private float cooldownTimer = 0f;
    private MonsterState state = MonsterState.Idle;

    // 缓存本对象的 2D 触发器
    Collider2D detectionCollider;

    void Awake()
    {
        detectionCollider = GetComponent<Collider2D>();
        // 确保 Collider2D 已经设置为 Is Trigger
        detectionCollider.isTrigger = true;
    }

    void Update()
    {
        switch (state)
        {
            case MonsterState.Idle:
                if (playerInRange)
                {
                    state = MonsterState.Attacking;
                    animator.SetTrigger("attack");
                }
                break;

            case MonsterState.Attacking:
                if (!playerInRange)
                {
                    // 玩家离开攻击范围，打断攻击
                    animator.ResetTrigger("attack");
                    state = MonsterState.Idle;
                }
                break;

            case MonsterState.Cooldown:
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= cooldownTime)
                {
                    cooldownTimer = 0f;
                    state = MonsterState.Idle;
                }
                break;
        }
    }

    // 动画事件调用：在攻击动画中调用这个函数
    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        if (audioSource != null && shootClip != null)
        {
            audioSource.pitch = Random.Range(pitchMin, pitchMax); // 随机 pitch
            audioSource.PlayOneShot(shootClip);
        }
    }

    // 动画播放完之后调用（也可在 Animator 设置一个 Event）
    public void OnAttackAnimationFinished()
    {
        state = MonsterState.Cooldown;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    // —— 以下为新增的 Gizmos 可视化逻辑 —— //

    void OnDrawGizmos()
    {
        // 在 Scene 视图里可视化“玩家检测范围”（2D 触发器的边框）
        // 如果当前对象没挂 Collider2D，就什么也不画
        if (detectionCollider == null)
        {
            detectionCollider = GetComponent<Collider2D>();
            if (detectionCollider == null) return;
        }

        Gizmos.color = new Color(1f, 0f, 0f, 0.8f); // 半透明红色

        // 针对 CircleCollider2D 画圆
        if (detectionCollider is CircleCollider2D circle)
        {
            Vector3 center = transform.position + (Vector3)circle.offset;
            float radius = circle.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
            Gizmos.DrawWireSphere(center, radius);
        }
        // 针对 BoxCollider2D 画矩形
        else if (detectionCollider is BoxCollider2D box)
        {
            Vector3 center = transform.position + (Vector3)box.offset;
            Vector3 size = new Vector3(
                box.size.x * transform.localScale.x,
                box.size.y * transform.localScale.y,
                0f
            );
            Gizmos.DrawWireCube(center, size);
        }
        // 针对其他 2D Collider（PolygonCollider2D、CapsuleCollider2D 等），画其包围盒
        else
        {
            Bounds bounds = detectionCollider.bounds;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}
