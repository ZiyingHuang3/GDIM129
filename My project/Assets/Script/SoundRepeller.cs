using UnityEngine;

public class SoundRepeller : MonoBehaviour
{
    [Header("Monster")]
    public MonsterMovementController monster;

    [Header("Battery")]
    public MonitorBatteryController batteryController;

    [Header("Settings")]
    public float batteryCost = 15f;
    public float cooldown = 5f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] repelSounds;

    private bool canUse = true;

    public void UseRepeller()
    {
        if (!canUse) return;


        if (batteryController.currentBattery < batteryCost)
            return;


        batteryController.UseBattery(batteryCost);


        monster.RetreatOneStage();

        if (audioSource != null && repelSounds != null)
        {
            if (repelSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, repelSounds.Length);

                audioSource.PlayOneShot(repelSounds[randomIndex]);
            }
        }

        StartCoroutine(CooldownRoutine());
    }

    System.Collections.IEnumerator CooldownRoutine()
    {
        canUse = false;

        yield return new WaitForSeconds(cooldown);

        canUse = true;
    }
}