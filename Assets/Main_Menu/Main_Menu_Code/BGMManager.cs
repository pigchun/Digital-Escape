using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource; // 拖入你的 AudioSource 组件
    public AudioClip mainMenuBGM; // 主菜单背景音乐
    public AudioClip gameBGM; // 游戏内背景音乐

    private void Start()
    {
        // 初始化播放主菜单背景音乐
        PlayMainMenuBGM();
    }

    // 播放主菜单背景音乐
    public void PlayMainMenuBGM()
    {
        if (audioSource != null && mainMenuBGM != null)
        {
            audioSource.clip = mainMenuBGM;
            audioSource.Play();
        }
    }

    // 播放游戏内背景音乐
    public void PlayGameBGM()
    {
        if (audioSource != null && gameBGM != null)
        {
            audioSource.clip = gameBGM;
            audioSource.Play();
        }
    }
}
