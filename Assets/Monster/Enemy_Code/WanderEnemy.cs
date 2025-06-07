using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WanderEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public LayerMask wallLayer;            // 墙的图层
    public float checkDistance = 0.5f;     // 前方检测距离

    private Vector2 moveDirection;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        // 移动
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 碰到墙就换方向
        if (IsWallAhead())
        {
            ChooseNewDirection();
        }
    }

    // 检测前方是否有墙
    bool IsWallAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, checkDistance, wallLayer);
        return hit.collider != null;
    }

    // 随机选择上下左右方向
    void ChooseNewDirection()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: moveDirection = Vector2.up; break;
            case 1: moveDirection = Vector2.down; break;
            case 2: moveDirection = Vector2.left; break;
            case 3: moveDirection = Vector2.right; break;
        }
    }

    // 在 Scene 里画出当前方向（用于调试）
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(moveDirection * checkDistance));
    }
}

