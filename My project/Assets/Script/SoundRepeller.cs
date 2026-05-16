using UnityEngine;

public class SoundRepeller : MonoBehaviour
{
    [Header("Camera")]
    public CameraSwitcher cameraSwitcher;

    [Header("Monster By Camera")]
    public MonsterMovementController[] monsters;

    [Header("Battery")]
    public MonitorBatteryController batteryController;

    [Header("Settings")]
    public float batteryCost = 5f;
    public float cooldown = 3f;

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

        int index = cameraSwitcher.currentIndex;

        if (index >= 0 && index < monsters.Length && monsters[index] != null)
        {
            monsters[index].RetreatOneStage();
        }

        if (audioSource != null && repelSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, repelSounds.Length);
            audioSource.PlayOneShot(repelSounds[randomIndex]);
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