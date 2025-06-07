using UnityEngine;
using UnityEngine.AI;
using System;

public class RunNode : Node
{
    NavMeshAgent agent;
    Transform target;
    Action playRunAnim;
    SpriteRenderer spriteRenderer;

    public RunNode(NavMeshAgent agent, Transform target, Action playRunAnim, SpriteRenderer spriteRenderer = null)
    {
        this.agent = agent;
        this.target = target;
        this.playRunAnim = playRunAnim;
        this.spriteRenderer = spriteRenderer;
    }

    public override NodeStatus Evaluate()
    {
        if (agent == null || target == null)
            return NodeStatus.Failure;

        // 只取 X 和 Y 分量来计算距离
        Vector2 agentPos2D  = new Vector2(agent.transform.position.x,  agent.transform.position.y);
        Vector2 targetPos2D = new Vector2(target.position.x,           target.position.y);

        // 只有在平面距离大于 stoppingDistance 时才继续跑，否则保持站定
        float dist2D = Vector2.Distance(agentPos2D, targetPos2D);
        if (dist2D > agent.stoppingDistance)
        {
            agent.SetDestination(target.position);
        }

        playRunAnim?.Invoke();

        if (spriteRenderer != null)
            spriteRenderer.flipX = target.position.x < agent.transform.position.x;

        return NodeStatus.Running;
    }
}
