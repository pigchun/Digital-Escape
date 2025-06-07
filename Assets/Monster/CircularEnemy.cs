using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularEnemy : MonoBehaviour
{
    public Transform centerPoint;     
    public float radius = 2f;         
    public float rotationSpeed = 180f; 

    private float angle = 0f;

    void Update()
    {
        // 位置绕圆圈
        angle += rotationSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
        transform.position = centerPoint.position + offset;

        // 自转视觉
        transform.Rotate(0f, 0f, 180f * Time.deltaTime); // 每秒转180度
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}

