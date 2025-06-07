using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public GameObject optionsMenu; // 你的 PNG 背景的菜单
    public Slider volumeSlider; // 音量调节滑块
    private AudioSource backgroundMusic; // 背景音乐

    private void Start()
    {
        // 直接查找 BGMManager
        GameObject bgmObject = GameObject.Find("BGMManager");
        if (bgmObject != null)
        {
            backgroundMusic = bgmObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("BGMManager 未找到，请检查名称是否正确！");
        }

        // **检查 PlayerPrefs 里是否有保存的音量值**
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            // **如果没有存储音量，则设为默认值 0.5**
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            PlayerPrefs.Save();
        }

        // **应用存储的音量**
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = savedVolume;
        }

        // **同步滑动条**
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        optionsMenu.SetActive(false); // 默认隐藏菜单
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save(); // **确保数据存储**
        }
    }
}
