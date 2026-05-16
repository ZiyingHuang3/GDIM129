using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrokenCameraFlash : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;

    [Header("Scare Images By Broken Camera")]
    public GameObject[] scareImages;

    public float minDelay = 5f;
    public float maxDelay = 15f;
    public float flashDuration = 1f;

    private void Start()
    {
        foreach (GameObject img in scareImages)
        {
            if (img != null)
                img.SetActive(false);
        }

        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            List<int> brokenCameras = new List<int>();

            for (int i = 0; i < cameraSwitcher.brokenStates.Length; i++)
            {
                if (cameraSwitcher.brokenStates[i])
                {
                    brokenCameras.Add(i);
                }
            }

            if (brokenCameras.Count > 0)
            {
                int randomBrokenCamera = brokenCameras[Random.Range(0, brokenCameras.Count)];

                if (randomBrokenCamera < scareImages.Length && scareImages[randomBrokenCamera] != null)
                {
                    scareImages[randomBrokenCamera].SetActive(true);

                    yield return new WaitForSeconds(flashDuration);

                    scareImages[randomBrokenCamera].SetActive(false);
                }
            }
        }
    }
}