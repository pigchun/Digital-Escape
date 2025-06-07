using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource; // ������� AudioSource ���
    public AudioClip mainMenuBGM; // ���˵���������
    public AudioClip gameBGM; // ��Ϸ�ڱ�������

    private void Start()
    {
        // ��ʼ���������˵���������
        PlayMainMenuBGM();
    }

    // �������˵���������
    public void PlayMainMenuBGM()
    {
        if (audioSource != null && mainMenuBGM != null)
        {
            audioSource.clip = mainMenuBGM;
            audioSource.Play();
        }
    }

    // ������Ϸ�ڱ�������
    public void PlayGameBGM()
    {
        if (audioSource != null && gameBGM != null)
        {
            audioSource.clip = gameBGM;
            audioSource.Play();
        }
    }
}
