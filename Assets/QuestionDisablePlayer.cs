using UnityEngine;

public class QuestionCanvasController : MonoBehaviour
{
    public GameObject player;

    void OnEnable()
    {
        if (player != null)
        {
            player.SetActive(false);
        }
    }

    void OnDisable()
    {
        if (player != null)
        {
            player.SetActive(true);
        }
    }
}
