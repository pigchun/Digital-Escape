using System;
using System.Collections;
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

    // ✅ 可选：血量系统（当前不启用）
    public int maxHealth = 3;
    private int currentHealth;

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
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = attackRange;

        birthTime = Time.time;
        currentHealth = maxHealth;

        // 行为树结构
        var isDeadNode = new ConditionNode(() => isDead);
        var dead = new DeadNode(animator, transform, () => PlayAnimation(EnemyAnimState.Dead, "Dead"));

        var timeout = new TimerExceededNode(birthTime, 30f);
        var timeoutDie = new Sequence(new List<Node> { timeout, dead });

        var inAttackRange = new InAttackRangeNode(transform, player, attackRange);

        var attack = new AttackNode(
            () => PlayAnimation(EnemyAnimState.Attack, "Attack"),
            transform,
            player,
            agent,
            attackRange
        );
        var attackSequence = new Sequence(new List<Node> { inAttackRange, attack });

        var playerInRange = new PlayerInRangeNode(transform, player, chaseRange, chaseRange + 1f, () => isChasing);
        var run = new RunNode(agent, player, () =>
        {
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

    // ✅ 被攻击调用此方法（由子弹脚本调用）
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            return;
        }

        StartCoroutine(PlayHitThenResume());
    }

    // ✅ 播放 Hit 动画，0.1秒后恢复 AI
    private IEnumerator PlayHitThenResume()
    {
        agent.isStopped = true;

        PlayAnimation(EnemyAnimState.Idle, "Hit");

        yield return new WaitForSeconds(0.084f); // 建议与你的 Hit 动画一致

        agent.isStopped = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // 假设你给子弹设置了 Tag = "Bullet"
        {
            TakeDamage(1);
            Destroy(other.gameObject); // 摧毁子弹
        }
    }

}
