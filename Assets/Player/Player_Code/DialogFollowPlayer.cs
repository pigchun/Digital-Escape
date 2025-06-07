// DialogFollowPlayer.cs
using UnityEngine;

public class DialogFollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // 拖入玩家对象
    public Vector3 offset = new Vector3(0, 2f, 0); // 相对于玩家的位置

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }
    }
}

// 用法：
// 1. 将这个脚本挂在你的 Text 对象上（设置为 World Space 模式）
// 2. 在 Inspector 中把 player 拖进去（通常是你的 Player 对象）
// 3. 调整 offset 值让对话显示在角色上方

