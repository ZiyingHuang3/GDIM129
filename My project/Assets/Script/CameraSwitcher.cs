using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public RawImage monitorScreen;

    public RenderTexture[] cameraViews;

    public Button leftButton;
    public Button rightButton;

    [Header("Broken Camera")]
    public GameObject brokenImage;

    public bool[] brokenStates;
    public int currentIndex = 0;

    private void Start()
    {
        brokenStates = new bool[cameraViews.Length];

        ShowCamera(currentIndex);

        leftButton.onClick.AddListener(PreviousCamera);
        rightButton.onClick.AddListener(NextCamera);
    }

    private void ShowCamera(int index)
    {
        monitorScreen.texture = cameraViews[index];

        RefreshBrokenImage();
    }

    void RefreshBrokenImage()
    {
        if (brokenImage == null) return;

        brokenImage.SetActive(brokenStates[currentIndex]);
    }

    public void BreakCamera(int index)
    {
        brokenStates[index] = true;

        RefreshBrokenImage();
    }

    public void NextCamera()
    {
        currentIndex++;

        if (currentIndex >= cameraViews.Length)
        {
            currentIndex = 0;
        }

        ShowCamera(currentIndex);
    }

    public void PreviousCamera()
    {
        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = cameraViews.Length - 1;
        }

        ShowCamera(currentIndex);
    }
}