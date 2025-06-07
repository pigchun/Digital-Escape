// GPTDialogPool.cs
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class GPTDialogPool : MonoBehaviour
{
    [TextArea(2, 5)]
    public List<string> cachedLines = new List<string>
    {
        "As the ancient door opens, a breeze of forgotten magic flows through the maze...",
        "The door creaks, revealing a path once sealed for centuries.",
        "A faint light flickers behind the door, guiding your destiny.",
        "This door may lead forward, but your choices echo behind you.",
        "Beyond the door lies a test greater than the key you hold."
    };

    public TextMeshProUGUI dialogText;
    public float displayDuration = 3f; // 显示时间秒数

    public void ShowRandomLine()
    {
        if (cachedLines.Count == 0 || dialogText == null)
        {
            Debug.LogWarning("No dialog lines or Text UI not assigned.");
            return;
        }

        int index = Random.Range(0, cachedLines.Count);
        dialogText.text = cachedLines[index];
        dialogText.gameObject.SetActive(true);

        StartCoroutine(HideAfterSeconds());
    }

    IEnumerator HideAfterSeconds()
    {
        yield return new WaitForSeconds(displayDuration);
        dialogText.gameObject.SetActive(false);
    }
}

// 用法：
// 在 Door.cs 中引用 GPTDialogPool，然后调用 ShowRandomLine() 显示对话，3 秒后自动隐藏。"
// Added auto-hide after a few seconds + updated TextMeshPro visibility
