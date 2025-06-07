using UnityEngine;
using UnityEngine.AI;

public class EnemyNavAgent : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 4f; // ✅ 可设置追击范围
    [SerializeField] SpriteRenderer spriteRenderer;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;  // 2D游戏禁用自动旋转
        agent.updateUpAxis = false;    // 禁用Y轴控制
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // ✅ 只有在范围内才追击
        if (distance <= chaseRange)
        {
            agent.SetDestination(target.position);

            // ✅ Flip X：如果玩家在怪物左边，就翻转
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = target.position.x < transform.position.x;
            }
        }
        else
        {
            // 超出范围时停止追踪
            agent.ResetPath();
        }
    }
}
