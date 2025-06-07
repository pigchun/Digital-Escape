using UnityEngine;

public class InAttackRangeNode : Node
{
    Transform self, player;
    float attackRange;

    public InAttackRangeNode(Transform self, Transform player, float range)
    {
        this.self = self;
        this.player = player;
        this.attackRange = range;
    }

    public override NodeStatus Evaluate()
    {
        if (self == null || player == null)
            return NodeStatus.Failure;

        // 只取 X 和 Y 分量来计算距离
        Vector2 selfPos2D   = new Vector2(self.position.x,   self.position.y);
        Vector2 playerPos2D = new Vector2(player.position.x, player.position.y);
        float dist = Vector2.Distance(selfPos2D, playerPos2D);

        // 使用 <= 而非 <，让“正好等于攻击距离”时也触发
        return dist <= attackRange
            ? NodeStatus.Success
            : NodeStatus.Failure;
    }
}
