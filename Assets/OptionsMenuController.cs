using UnityEngine;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject optionsMenu; // 拖入新的 OptionsMenu 对象

    // 打开 OptionsMenu
    public void ShowOptionsMenu()
    {
        if (optionsMenu != null)
        {
            Debug.Log("打开 OptionsMenu");
            optionsMenu.SetActive(true); // 仅显示 OptionsMenu

            // 不暂停游戏时间，保持一切正常运行
            // 不再设置 Time.timeScale = 0;

        }
        else
        {
            Debug.LogWarning("OptionsMenu 没有正确分配！");
        }
    }

    // 关闭 OptionsMenu
    public void HideOptionsMenu()
    {
        if (optionsMenu != null)
        {
            Debug.Log("关闭 OptionsMenu");
            optionsMenu.SetActive(false); // 隐藏 OptionsMenu

            // 保持游戏时间继续
            // 不需要设置 Time.timeScale = 1;
        }
    }
}
