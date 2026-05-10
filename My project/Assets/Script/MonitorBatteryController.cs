using UnityEngine;
using UnityEngine.UI;

public class MonitorBatteryController : MonoBehaviour
{
    [Header("Monitor")]
    public GameObject cameraScreen;

    [Header("Battery")]
    public Slider batterySlider;
    public float maxBattery = 100f;
    public float drainSpeed = 5f;
    public MonsterMovementController monster;
    public float currentBattery;
    private bool monitorOpen = false;

    private void Start()
    {
        currentBattery = maxBattery;

        batterySlider.maxValue = maxBattery;
        batterySlider.value = currentBattery;


        cameraScreen.SetActive(false);
    }

    private void Update()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        ToggleMonitor();
    }

    if (monitorOpen)
    {
        currentBattery -= drainSpeed * Time.deltaTime;
    }
    else
    {
        currentBattery += drainSpeed * Time.deltaTime;
    }

    currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

    batterySlider.value = currentBattery;

    if (currentBattery <= 0 && monitorOpen)
    {
        CloseMonitor();
    }
}

    public void ToggleMonitor()
    {
        if (monitorOpen)
        {
            CloseMonitor();
        }
        else
        {
            OpenMonitor();
        }
    }

    public void OpenMonitor()
    {
        if (currentBattery <= 0) return;

        monitorOpen = true;


        cameraScreen.SetActive(true);
        if (monster != null)
        {
            monster.RetreatOneStage();
        }
    }

    public void CloseMonitor()
    {
        monitorOpen = false;

        cameraScreen.SetActive(false);
    }
    public void UseBattery(float amount)
    {
        currentBattery -= amount;

        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

        batterySlider.value = currentBattery;

        if (currentBattery <= 0)
        {
            CloseMonitor();
        }
    }
    public void BreakMonitor()
    {
        monitorOpen = false;
    }
}