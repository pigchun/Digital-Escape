using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComicTransition : MonoBehaviour
{
    public GameObject comicPanel;            // ��ɫ���� + ��������ͼ�ĸ�����
    public Image[] comicSlots;               // 4 ������ͼƬ
    public GameObject gameplayRoot;          // ��Ϸ������ĸ����壨������ҡ��Թ�����ʱ���ȣ�

    public float delayBetweenPanels = 1.2f;
    public float holdTimeAfterLast = 2f;

    public void StartTransition()
    {
        comicPanel.SetActive(true);         // �򿪺�ɫ���� + ��������
        gameplayRoot.SetActive(false);      // ��ʱ������Ϸ����
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

        // ���������ת����������ʾ��Ϸ����
        comicPanel.SetActive(false);
        gameplayRoot.SetActive(true);
    }
}
