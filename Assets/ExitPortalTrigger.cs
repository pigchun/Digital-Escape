using UnityEngine;

public class ExitPortalTrigger : MonoBehaviour
{
    public ComicTransition comicTransition; // ת���ű�
    public GameObject player;               // ��Ҷ���
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.gameObject == player)
        {
            triggered = true;

            // ����ת������
            comicTransition.StartTransition();
        }
    }
}
