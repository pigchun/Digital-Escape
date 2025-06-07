using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 5f; // 最长存在时间，防止卡住

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    if (other.CompareTag("Wall"))
    {
        Destroy(gameObject);
    }
}

}

