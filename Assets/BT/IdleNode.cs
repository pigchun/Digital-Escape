using System;

public class IdleNode : Node {
    Action playIdle;

    public IdleNode(Action playIdleAnim) {
        playIdle = playIdleAnim;
    }

    public override NodeStatus Evaluate() {
        playIdle?.Invoke();
        return NodeStatus.Success;
    }
}