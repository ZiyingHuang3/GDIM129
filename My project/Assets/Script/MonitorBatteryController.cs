using UnityEngine;
using UnityEngine.UI;

public class MonitorBatteryController : MonoBehaviour
{
    public GameObject cameraScreen;   
    public Slider batterySlider;      
    public float maxBattery = 100f;
    public float drainSpeed = 5f;

    private float currentBattery;
    private bool monitorOpen = false;

    private void Start()
    {
        currentBattery = maxBattery;
        batterySlider.maxValue = maxBattery;
        batterySlider.value = currentBattery;

        cameraScreen.SetActive(true); 
    }

    private void Update()
    {
        if (monitorOpen && currentBattery > 0)
        {
            currentBattery -= drainSpeed * Time.deltaTime;
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
            batterySlider.value = currentBattery;
        }

        if (currentBattery <= 0)
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
        cameraScreen.SetActive(false);
    }

    public void CloseMonitor()
    {
        monitorOpen = false;
        cameraScreen.SetActive(true);
    }
}
