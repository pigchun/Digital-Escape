using UnityEngine;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject optionsMenu; // �����µ� OptionsMenu ����

    // �� OptionsMenu
    public void ShowOptionsMenu()
    {
        if (optionsMenu != null)
        {
            Debug.Log("�� OptionsMenu");
            optionsMenu.SetActive(true); // ����ʾ OptionsMenu

            // ����ͣ��Ϸʱ�䣬����һ����������
            // �������� Time.timeScale = 0;

        }
        else
        {
            Debug.LogWarning("OptionsMenu û����ȷ���䣡");
        }
    }

    // �ر� OptionsMenu
    public void HideOptionsMenu()
    {
        if (optionsMenu != null)
        {
            Debug.Log("�ر� OptionsMenu");
            optionsMenu.SetActive(false); // ���� OptionsMenu

            // ������Ϸʱ�����
            // ����Ҫ���� Time.timeScale = 1;
        }
    }
}
