using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementController : MonoBehaviour
{
    public Animator monsterAnimator;

    [Header("Stage Positions")]
    public Transform farPoint;
    public Transform closePoint;
    public Transform runPoint;

    [Header("Move")]
    public float moveSpeed = 3f;
    public float moveInterval = 5f;

    [Header("Attack")]
    public int damage = 20;

    private int currentStage = 0;
    private float timer;
    private Transform targetPoint;
    [Header("Jumpscare")]
    public Transform jumpscarePoint;
    public AudioSource audioSource;
    public AudioClip jumpscareSound;
    private bool isJumpscaring = false;
    public float jumpscareDuration = 1.5f;

    [Header("Broken Screen")]
    public GameObject thisCameraBrokenScreen;
    public CameraSwitcher cameraManager;
    public MonitorBatteryController batteryController;
    [Header("Camera")]
    public int cameraIndex;
    public int monsterCameraIndex = 1;
    private void Start()
    {
        timer = moveInterval;
        SetStage(0);
    }

    private void Update()
    {
        if (isJumpscaring) return;

        MoveToTarget();

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            AdvanceStage();
            timer = moveInterval;
        }
    }

    void MoveToTarget()
    {
        if (targetPoint == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );
    }

    void AdvanceStage()
    {
        currentStage++;

        if (currentStage >= 3)
        {
            StartCoroutine(JumpscareRoutine());
            return;
        }

        SetStage(currentStage);
    }

    public void RetreatOneStage()
    {
        currentStage--;

        if (currentStage < 0)
            currentStage = 0;

        SetStage(currentStage);
        timer = moveInterval;
    }

    void SetStage(int stage)
    {
        currentStage = stage;

        if (stage == 0)
            targetPoint = farPoint;
        else if (stage == 1)
            targetPoint = closePoint;
        else if (stage == 2)
            targetPoint = runPoint;

        monsterAnimator.SetInteger("Stage", currentStage);
    }

    IEnumerator JumpscareRoutine()
    {
        isJumpscaring = true;

        targetPoint = null;

        transform.position = jumpscarePoint.position;
        transform.rotation = jumpscarePoint.rotation;

        monsterAnimator.SetTrigger("Jumpscare");

        if (audioSource != null && jumpscareSound != null)
        {
            audioSource.PlayOneShot(jumpscareSound);
        }

        yield return new WaitForSeconds(jumpscareDuration);

        if (cameraManager != null)
        {
            cameraManager.BreakCamera(monsterCameraIndex);
        }
        if (batteryController != null)
        {
            batteryController.BreakMonitor();
        }

    }
}
