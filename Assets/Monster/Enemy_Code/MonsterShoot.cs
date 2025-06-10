using UnityEngine;

public class MonsterShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;
    public float bulletSpeed = 5f;

    private Collider2D detectionCollider;
    private float lastShootTime = 0f;
    private Transform player;

    private Animator animator;

    void Awake()
    {
        // ✅ 获取 DetectionZone 子物体
        Transform detectionZone = transform.Find("DetectionZone");
        if (detectionZone != null)
        {
            detectionCollider = detectionZone.GetComponent<Collider2D>();
            detectionCollider.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("❗ DetectionZone not found!");
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ✅ 如果检测到玩家，并且冷却时间到了，就播放攻击动画
        if (player != null && Time.time - lastShootTime >= shootCooldown)
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("attack");
            lastShootTime = Time.time;
        }
    }

    // ✅ 这个函数由动画事件触发（需绑定到 Attack 动画中）
    public void Shoot()
    {
        if (player == null || bulletPrefab == null || firePoint == null)
            return;

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        Debug.Log("🔫 MonsterShoot → Shoot() called by animation.");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            Debug.Log("🧿 Player entered detection range");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.transform == player)
        {
            player = null;
            Debug.Log("🧊 Player left detection range");
        }
    }

    // ✅ 供 DetectionZone 子物体调用（可选使用代理脚本）
    public void OnPlayerEnter(Transform target)
    {
        player = target;
    }

    public void OnPlayerExit(Transform target)
    {
        if (player == target)
            player = null;
    }

    // ✅ 可视化检测范围（Gizmos）
    void OnDrawGizmosSelected()
    {
        Transform zone = transform.Find("DetectionZone");
        if (zone != null)
        {
            Collider2D col = zone.GetComponent<Collider2D>();
            if (col is CircleCollider2D circle)
            {
                Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.4f);
                Gizmos.DrawWireSphere(circle.transform.position, circle.radius * circle.transform.lossyScale.x);
            }
            else if (col is BoxCollider2D box)
            {
                Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.4f);
                Gizmos.DrawWireCube(box.transform.position, box.size);
            }
        }
    }
}
