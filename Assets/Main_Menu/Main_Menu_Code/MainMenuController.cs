using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public ComicTransition comicTransition;

    public void StartGame()
    {
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);  // �������˵�

        if (comicTransition != null)
            comicTransition.StartTransition(); // ����ת������
    }
}
