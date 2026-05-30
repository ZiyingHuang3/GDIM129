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

    public bool gameEnded = false;
    public CameraSwitcher cameraSwitcher;
    public GameObject goodEndingPanel;
    public GameObject normalEndingPanel;
    public GameObject badEndingPanel;
    public AudioSource bgmSource;
    public AudioSource endingMusicSource;
    public AudioClip goodEndingMusic;
    public AudioClip normalEndingMusic;
    public AudioClip badEndingMusic;
    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        if (timer >= realSecondsPerGameMinute)
        {
            timer -= realSecondsPerGameMinute;

            AdvanceMinute();
        }
    }

    void AdvanceMinute()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        UpdateUI();

        if (hour >= 6)
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        int displayHour;

        if (hour == 0)
            displayHour = 12;
        else
            displayHour = hour;

        timeText.text =
            displayHour
            + " AM";
    }
    int GetBrokenCameraCount()
    {
        int count = 0;

        foreach (bool broken in cameraSwitcher.brokenStates)
        {
            if (broken)
                count++;
        }

        return count;
    }

    void PlayEndingMusic(AudioClip clip)
    {
        if (endingMusicSource == null || clip == null) return;

        endingMusicSource.clip = clip;
        endingMusicSource.loop = false;
        endingMusicSource.Play();
    }
    void StopAllGameAudio()
    {
        AudioSource[] allAudio = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAudio)
        {
            audio.Stop();
        }
    }
    public void BadEnding()
    {
        if (gameEnded) return;

        gameEnded = true;

        StopAllGameAudio();

        badEndingPanel.SetActive(true);
        PlayEndingMusic(badEndingMusic);

        Time.timeScale = 0f;
    }


    void EndGame()
    {
        gameEnded = true;
        timeText.text = "6 AM";

        StopAllGameAudio();

        int brokenCount = GetBrokenCameraCount();

        if (brokenCount == 0)
        {
            goodEndingPanel.SetActive(true);
            PlayEndingMusic(goodEndingMusic);
        }
        else if (brokenCount < 4)
        {
            normalEndingPanel.SetActive(true);
            PlayEndingMusic(normalEndingMusic);
        }
        else
        {
            badEndingPanel.SetActive(true);
            PlayEndingMusic(badEndingMusic);
        }

        Time.timeScale = 0f;
    }
}