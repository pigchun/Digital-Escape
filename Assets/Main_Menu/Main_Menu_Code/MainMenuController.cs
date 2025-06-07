using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public ComicTransition comicTransition;

    public void StartGame()
    {
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);  // 隐藏主菜单

        if (comicTransition != null)
            comicTransition.StartTransition(); // 启动转场动画
    }
}
