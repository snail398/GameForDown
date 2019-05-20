using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // Hero life
    [SerializeField] private Text lifeCountText;
    [SerializeField] private IntVariable lifeCount;

    // Player Race Score
    [SerializeField] private Text scoreCountText;
    [SerializeField] private IntVariable scoreCount;


    // Player Max Score
    [SerializeField] private Text maxScoreCountText;
    [SerializeField] private IntVariable maxScoreCount;

    [SerializeField] private CanvasGroup canvasGroup;
    void Awake()
    {
        SetMaxScore();
    }

    private void SetMaxScore()
    {
        maxScoreCountText.text = maxScoreCount.Value.ToString();
    }

    public void OnHpChanged()
    {
        lifeCountText.text = lifeCount.Value.ToString();
    }

    void Update()
    {
        scoreCountText.text = scoreCount.Value.ToString();
    }

    public void ShowTable()
    {
        canvasGroup.alpha = 1;
    }

    public void HideTable()
    {
        canvasGroup.alpha = 0;
    }
}
