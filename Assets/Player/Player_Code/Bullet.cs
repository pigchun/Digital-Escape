using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime); // 飞行一段时间后自动销毁
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 判断碰撞目标是否为敌人（示例）
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            // 你也可以在这里触发敌人受伤逻辑
        }
    }
}

