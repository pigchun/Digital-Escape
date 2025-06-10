using UnityEngine;

public class DetectionZoneTrigger : MonoBehaviour
{
    private MonsterShoot monsterShoot;

    void Start()
    {
        monsterShoot = GetComponentInParent<MonsterShoot>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            monsterShoot.OnPlayerEnter(other.transform);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            monsterShoot.OnPlayerExit(other.transform);
    }
}
