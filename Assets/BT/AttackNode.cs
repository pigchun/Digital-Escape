using UnityEngine;
using System;
using UnityEngine.AI;

/// <summary>
/// 行为树节点：当怪物播放攻击动画最后一帧时，如果玩家在 2D 平面距离 ≤ damageRange，就调用 TakeDamage。
/// 使用 Vector2 只比较 X/Y，忽略 Z 分量，确保 2D 场景下距离判断正确。
/// </summary>
public class AttackNode : Node
{
    Action playAttack;
    float attackDuration = 0.35f;
    float attackStartTime;
    bool isAttacking = false;

    Transform self;
    Transform player;
    float damageRange = 1.5f;
    PlayerHealth playerHealth;
    NavMeshAgent agent;
    bool hasHit = false;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="playAttackAnim">播放攻击动画的委托</param>
    /// <param name="self">怪物的 Transform</param>
    /// <param name="player">玩家的 Transform</param>
    /// <param name="agent">怪物所挂的 NavMeshAgent</param>
    /// <param name="damageRange">伤害判定范围（2D 平面距离）</param>
    public AttackNode(Action playAttackAnim, Transform self, Transform player, NavMeshAgent agent, float damageRange = 1.5f)
    {
        this.playAttack = playAttackAnim;
        this.self = self;
        this.player = player;
        this.agent = agent;
        this.damageRange = damageRange;
        this.playerHealth = player.GetComponent<PlayerHealth>();
    }

    public override NodeStatus Evaluate()
    {
        if (!isAttacking)
        {
            // 停下 NavMeshAgent 再攻击
            if (agent != null)
                agent.ResetPath();

            playAttack?.Invoke();
            attackStartTime = Time.time;
            isAttacking = true;
            hasHit = false;
            return NodeStatus.Running;
        }

        // 等待动画播完
        if (Time.time - attackStartTime < attackDuration)
        {
            return NodeStatus.Running;
        }
        else
        {
            if (!hasHit)
            {
                // 只取 X 和 Y 分量计算平面距离
                Vector2 selfPos2D   = new Vector2(self.position.x,   self.position.y);
                Vector2 playerPos2D = new Vector2(player.position.x, player.position.y);
                float dist2D = Vector2.Distance(selfPos2D, playerPos2D);

                // 动画最后一帧再判定 ≤ damageRange
                if (dist2D <= damageRange && playerHealth != null && !playerHealth.IsDead())
                {
                    playerHealth.TakeDamage(1);
                    Debug.Log("💥 Enemy hit player at END of attack animation!");
                }
                hasHit = true;
            }

            isAttacking = false;
            return NodeStatus.Success;
        }
    }
}
