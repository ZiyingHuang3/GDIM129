using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public RawImage monitorScreen;

    public RenderTexture[] cameraViews;

    public Button leftButton;
    public Button rightButton;

    private int currentIndex = 0;

    private void Start()
    {
        ShowCamera(currentIndex);

        leftButton.onClick.AddListener(PreviousCamera);
        rightButton.onClick.AddListener(NextCamera);
    }

    private void ShowCamera(int index)
    {
        monitorScreen.texture = cameraViews[index];
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