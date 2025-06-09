using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    private bool isDead = false;

    public PlayerMovement movementScript; 
    public HealthUI healthUI;
    public Animator animator; 
    public SpriteRenderer spriteRenderer; 
    public GameObject hitEffectPrefab;

    
    public float flashDuration = 0.1f;
    public int flashCount = 5;

    
    public float invincibleTime = 1f;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthUI.UpdateHearts(currentHealth);
        
    }

    public void TakeDamage(int amount)
    {
        if (isDead || isInvincible) return;

        currentHealth -= amount;
        Debug.Log("curr health：" + currentHealth);
        healthUI.UpdateHearts(currentHealth);
        SpawnHitEffect();

        StartCoroutine(FlashCoroutine());
        StartCoroutine(InvincibilityCoroutine());

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player Dead");

        // —— 在这里把 Rigidbody2D 的速度清零 —— //
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
        }

        // 播放死亡动画
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // 禁用移动脚本
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        // （可选）把角色的碰撞体设成 Kinematic，避免再被移动
        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.3f); // 半透明
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = new Color(1, 1, 1, 1f);   // 恢复正常
            yield return new WaitForSeconds(flashDuration);
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void SpawnHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}

