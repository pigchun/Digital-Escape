using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyAnimState { Idle, Run, Attack, Dead }

public class EnemyBT : MonoBehaviour
{
    public NavMeshAgent agent;
    public SpriteRenderer spriteRenderer;
    public Transform player;
    public float chaseRange = 4f;
    public float speed = 2f;
    public float attackRange = 1f;
    public Animator animator;

    Node root;
    float birthTime;
    bool isDead = false;
    bool isChasing = false;
    EnemyAnimState currentAnimState = EnemyAnimState.Idle;

    void PlayAnimation(EnemyAnimState newState, string animName)
    {
        if (currentAnimState != newState)
        {
            animator.Play(animName);
            currentAnimState = newState;
        }
    }

    void Start()
    {
        // 禁用自动旋转/上下轴，适用于2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // 关键1：保证怪物会在“攻击距离”停下
        agent.stoppingDistance = attackRange;

        birthTime = Time.time;

        var isDeadNode = new ConditionNode(() => isDead);
        var dead = new DeadNode(animator, transform, () => PlayAnimation(EnemyAnimState.Dead, "Dead"));

        var timeout = new TimerExceededNode(birthTime, 30f);
        var timeoutDie = new Sequence(new List<Node> { timeout, dead });

        // 关键2：InAttackRangeNode 改成 ≤ 判断
        var inAttackRange = new InAttackRangeNode(transform, player, attackRange);

        // 关键3：AttackNode 要传入 NavMeshAgent
        var attack = new AttackNode(
            () => PlayAnimation(EnemyAnimState.Attack, "Attack"),
            transform,
            player,
            agent,           // 这里传agent
            attackRange      // 攻击范围
        );
        var attackSequence = new Sequence(new List<Node> { inAttackRange, attack });

        var playerInRange = new PlayerInRangeNode(transform, player, chaseRange, chaseRange + 1f, () => isChasing);
        var run = new RunNode(agent, player, () => {
            isChasing = true;
            PlayAnimation(EnemyAnimState.Run, "Run");
        }, spriteRenderer);
        var chaseSequence = new Sequence(new List<Node> { playerInRange, run });

        var idle = new IdleNode(() =>
        {
            isChasing = false;
            PlayAnimation(EnemyAnimState.Idle, "Idle");
        });

        root = new Selector(new List<Node> {
            new Sequence(new List<Node> { isDeadNode, dead }),
            timeoutDie,
            attackSequence,
            chaseSequence,
            idle
        });
    }

    void Update()
    {
        if (!isDead)
        {
            root.Evaluate();
        }
    }

    void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
