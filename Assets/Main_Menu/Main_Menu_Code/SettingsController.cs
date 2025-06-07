using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public GameObject optionsMenu; // ��� PNG �����Ĳ˵�
    public Slider volumeSlider; // �������ڻ���
    private AudioSource backgroundMusic; // ��������

    private void Start()
    {
        // ֱ�Ӳ��� BGMManager
        GameObject bgmObject = GameObject.Find("BGMManager");
        if (bgmObject != null)
        {
            backgroundMusic = bgmObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("BGMManager δ�ҵ������������Ƿ���ȷ��");
        }

        // **��� PlayerPrefs ���Ƿ��б��������ֵ**
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            // **���û�д洢����������ΪĬ��ֵ 0.5**
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            PlayerPrefs.Save();
        }

        // **Ӧ�ô洢������**
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = savedVolume;
        }

        // **ͬ��������**
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        optionsMenu.SetActive(false); // Ĭ�����ز˵�
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
            PlayerPrefs.Save(); // **ȷ�����ݴ洢**
        }
    }
}
