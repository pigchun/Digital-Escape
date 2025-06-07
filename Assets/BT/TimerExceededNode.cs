using System;
using UnityEngine;

public class TimerExceededNode : Node {
    float startTime, maxTime;

    public TimerExceededNode(float startTime, float maxTime) {
        this.startTime = startTime;
        this.maxTime = maxTime;
    }

    public override NodeStatus Evaluate() {
        return Time.time - startTime > maxTime ? NodeStatus.Success : NodeStatus.Failure;
    }
}