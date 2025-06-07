using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuicideEnemy : MonoBehaviour
{
    [Header("Blast setup")]
    public float explosionRadius = 2f;
    public float delayBeforeExplode = 2f;
    public int damage = 1;

    [Header("Animation and sound effects")]
    public Animator animator;                
    public AudioClip explosionSound;         

    [Header("flicker warning")]
    public SpriteRenderer spriteRenderer;    
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    public float flashSpeed = 0.2f;

    private bool hasExploded = false;
    private Coroutine flashingCoroutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasExploded)
        {
            hasExploded = true;

            
            if (flashingCoroutine == null)
                flashingCoroutine = StartCoroutine(FlashWarningColor());

            StartCoroutine(ExplodeAfterDelay());
        }
    }


    public GameObject explosionPrefab;  

    public void SpawnExplosion()
    {
        // 正常方向爆炸
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // FlipY 方向爆炸
        GameObject flipped = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        SpriteRenderer sr = flipped.GetComponentInChildren<SpriteRenderer>(); // ✅ 更稳
        if (sr != null)
        {
            sr.flipY = true;
        }
    }



    IEnumerator FlashWarningColor()
    {
        bool isRed = false;
        while (true)
        {
            spriteRenderer.color = isRed ? warningColor : normalColor;
            isRed = !isRed;
            yield return new WaitForSeconds(flashSpeed);
        }
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeExplode);

        
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            flashingCoroutine = null;
        }

        spriteRenderer.color = normalColor;
        SpawnExplosion();

        
        if (animator != null)
            animator.SetTrigger("Dead");


        
        if (explosionSound != null)
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        
    }

    
    public void DealDamageAndDestroy()
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth health = hit.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}


