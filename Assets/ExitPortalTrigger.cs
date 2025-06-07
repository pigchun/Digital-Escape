using UnityEngine;

public class ExitPortalTrigger : MonoBehaviour
{
    public ComicTransition comicTransition; // 转场脚本
    public GameObject player;               // 玩家对象
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.gameObject == player)
        {
            triggered = true;

            // 触发转场动画
            comicTransition.StartTransition();
        }
    }
}
