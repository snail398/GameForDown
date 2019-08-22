using UnityEngine;

public class LoadingScreenView : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }
    public void ShowLoaingScreen()
    {
        _canvasGroup.alpha = 1;
    }

    public void HideLoadingScreen()
    {
        if (_canvasGroup != null)
        _canvasGroup.alpha = 0;
    }
}
