using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;
    public float score;
    public int coins = 0;
    public int extraTime = 0;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinsText;

    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else{
                TimeLeft = 0;
                TimerOn = false;
                Application.Quit();
            
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime);
        timerText.text = string.Format("Time: {0:00}", seconds);
    }


    public void finishLevel()
    {
        score += TimeLeft * 50;
        TimeLeft = 0;
        scoreText.text = string.Format("Score: {0:00}", score);
        TimerOn = false;
    }

    public void startLevel()
    {
        TimeLeft = 300 + extraTime;
        TimerOn = true;
    }

    public void getCoin()
    {
        score += 50;
        coins += 1;
        scoreText.text = string.Format("Score: {0:00}", score);
        coinsText.text = string.Format("{0} X", coins);
    }

    public void buyItem(int amt)
    {
        coins -= amt;
        coinsText.text = string.Format("{0} X", coins);
    }
}

