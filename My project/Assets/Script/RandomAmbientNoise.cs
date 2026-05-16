using UnityEngine;
using System.Collections;

public class RandomAmbientNoise : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] randomSounds;

    public float minDelay = 8f;
    public float maxDelay = 20f;

    private void Start()
    {
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);

            yield return new WaitForSeconds(waitTime);

            if (randomSounds.Length > 0)
            {
                int index = Random.Range(0, randomSounds.Length);

                audioSource.PlayOneShot(randomSounds[index]);
            }
        }
    }
}