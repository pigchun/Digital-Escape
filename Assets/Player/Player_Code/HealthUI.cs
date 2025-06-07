using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] hearts;

    public void UpdateHearts(int currentHealth)
    {
        //Debug.Log("更新血量UI：" + currentHealth);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}

