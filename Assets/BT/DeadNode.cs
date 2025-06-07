using UnityEngine;
using System;

public class DeadNode : Node {
    Animator animator;
    bool played = false;
    float deathStartTime;
    float deathDuration = 1f;
    Transform enemy;
    Action playDead;

    public DeadNode(Animator anim, Transform self, Action playDeadAnim) {
        animator = anim;
        enemy = self;
        playDead = playDeadAnim;
    }

    public override NodeStatus Evaluate() {
        if (!played) {
            playDead?.Invoke();
            deathStartTime = Time.time;
            played = true;
        }

        if (Time.time - deathStartTime >= deathDuration) {
            GameObject.Destroy(enemy.gameObject);
            return NodeStatus.Success;
        }

        return NodeStatus.Running;
    }
}