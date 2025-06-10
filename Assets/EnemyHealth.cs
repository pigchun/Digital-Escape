using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth}");

        if (animator != null)
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("isHit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        // 等待死亡动画播放完后销毁（建议动画设置好长度）
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // 等待动画长度，假设死亡动画 1 秒，可改成变量
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
