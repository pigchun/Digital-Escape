using System;
using System.Collections.Generic;

public enum NodeStatus { Success, Failure, Running }

public abstract class Node {
    public abstract NodeStatus Evaluate();
}

public class Sequence : Node {
    private List<Node> children;
    public Sequence(List<Node> nodes) => children = nodes;

    public override NodeStatus Evaluate() {
        foreach (var node in children) {
            var status = node.Evaluate();
            if (status != NodeStatus.Success)
                return status;
        }
        return NodeStatus.Success;
    }
}

public class Selector : Node {
    private List<Node> children;
    public Selector(List<Node> nodes) => children = nodes;

    public override NodeStatus Evaluate() {
        foreach (var node in children) {
            var status = node.Evaluate();
            if (status == NodeStatus.Success || status == NodeStatus.Running)
                return status;
        }
        return NodeStatus.Failure;
    }
}

public class ConditionNode : Node {
    private Func<bool> condition;
    public ConditionNode(Func<bool> cond) => condition = cond;

    public override NodeStatus Evaluate() {
        return condition() ? NodeStatus.Success : NodeStatus.Failure;
    }
}