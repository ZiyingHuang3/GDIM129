using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public AudioSource bgm;

    private void Start()
    {
        tutorialPanel.SetActive(true);

        if (bgm != null)
            bgm.Pause();

        Time.timeScale = 0f;
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);

        if (bgm != null)
            bgm.Play();

        Time.timeScale = 1f;
    }
}