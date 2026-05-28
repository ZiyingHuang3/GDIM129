using UnityEngine;
using TMPro;

public class GameTimeManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timeText;

    [Header("Time")]
    public float realSecondsPerGameMinute = 0.5f;

    private float timer;

    private int hour = 0;
    private int minute = 0;

    public bool gameEnded=false;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        if(timer >= realSecondsPerGameMinute)
        {
            timer -= realSecondsPerGameMinute;

            AdvanceMinute();
        }
    }

    void AdvanceMinute()
    {
        minute++;

        if(minute >=60)
        {
            minute=0;
            hour++;
        }

        UpdateUI();

        if(hour >=6)
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        int displayHour;

        if(hour==0)
            displayHour=12;
        else
            displayHour=hour;

        timeText.text =
            displayHour.ToString("00")
            + ":"
            + minute.ToString("00")
            + " AM";
    }

    void EndGame()
    {
        gameEnded=true;

        timeText.text="06:00 AM";

        Debug.Log("YOU SURVIVED");

    }
}