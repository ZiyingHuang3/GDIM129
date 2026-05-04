using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;

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
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }

    public void NextCamera()
    {
        currentIndex++;

        if (currentIndex >= cameras.Length)
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
            currentIndex = cameras.Length - 1;
        }

        ShowCamera(currentIndex);
    }

    public int GetCurrentCameraIndex()
    {
        return currentIndex;
    }
}
