using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComicTransition : MonoBehaviour
{
    public GameObject comicPanel;            // 黑色背景 + 所有漫画图的父物体
    public Image[] comicSlots;               // 4 张漫画图片
    public GameObject gameplayRoot;          // 游戏主界面的父物体（例如玩家、迷宫、计时器等）

    public float delayBetweenPanels = 1.2f;
    public float holdTimeAfterLast = 2f;

    public void StartTransition()
    {
        comicPanel.SetActive(true);         // 打开黑色背景 + 漫画容器
        gameplayRoot.SetActive(false);      // 暂时隐藏游戏画面
        StartCoroutine(PlayComics());
    }

    IEnumerator PlayComics()
    {
        foreach (Image img in comicSlots)
        {
            img.gameObject.SetActive(true);
            img.canvasRenderer.SetAlpha(0f);
            img.CrossFadeAlpha(1f, 0.6f, false);
            yield return new WaitForSeconds(delayBetweenPanels);
        }

        yield return new WaitForSeconds(holdTimeAfterLast);

        // 播完后隐藏转场动画，显示游戏内容
        comicPanel.SetActive(false);
        gameplayRoot.SetActive(true);
    }
}
