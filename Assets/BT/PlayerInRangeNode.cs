using UnityEngine;
using System;

public class PlayerInRangeNode : Node {
    Transform self, player;
    float enterRange, exitRange;
    Func<bool> isChasingGetter;

    public PlayerInRangeNode(Transform self, Transform player, float enterRange, float exitRange, Func<bool> isChasingGetter) {
        this.self = self;
        this.player = player;
        this.enterRange = enterRange;
        this.exitRange = exitRange;
        this.isChasingGetter = isChasingGetter;
    }

    public override NodeStatus Evaluate() {
        float dist = Vector3.Distance(self.position, player.position);
        bool isChasing = isChasingGetter();

        if (dist < enterRange || (isChasing && dist < exitRange))
            return NodeStatus.Success;

        return NodeStatus.Failure;
    }
}