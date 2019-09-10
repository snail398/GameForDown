using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDController : MonoBehaviour
{
    public struct Ctx
    {
        public PlayersData playersData;
        public AnaliticsCore analitics;
    }

    // Hero life
    [SerializeField] private Image oneLifePrefab;
    [SerializeField] private RectTransform lifeContainer;
    [SerializeField] private IntVariable lifeCount;

    // Player Race Score
    [SerializeField] private Text scoreCountText;
    [SerializeField] private IntVariable scoreCount;
    
    // Player Max Score
    [SerializeField] private Text maxScoreCountText;
    [SerializeField] private IntVariable maxScoreCount;

    // Coin Score
    [SerializeField] private Text coinCountText;
    [SerializeField] private IntVariable coinCount;

    [SerializeField] private CanvasGroup canvasGroup;

    private Ctx _ctx;
    private readonly List<CanvasGroup> _lifeList = new List<CanvasGroup>();
    private DateTime _startTime;

    public void SetCtx(Ctx ctx)
    {
        _ctx = ctx;
        coinCount.Value = _ctx.playersData.GetCoinsCount();
        SetMaxScore();
    }
    private void Awake()
    {
        CreateLife();
        _startTime = DateTime.UtcNow;
    }

    private void SetMaxScore()
    {
        maxScoreCount.Value = _ctx.playersData.GetMaxScore();
        maxScoreCountText.text = AddZeroBeforeValue(maxScoreCount.Value);
    }

    public void OnHpChanged()
    {
        RenderHpCount(lifeCount.Value);
    }

    private void Update()
    {
        scoreCountText.text = AddZeroBeforeValue(scoreCount.Value);
        coinCountText.text = coinCount.Value.ToString();
    }

    public void ShowTable()
    {
        canvasGroup.alpha = 1;
    }

    public void HideTable()
    {
        canvasGroup.alpha = 0;
    }

    public void SaveCoinsCount()
    {
        _ctx.playersData.SetCointCount(coinCount.Value);
        _ctx.playersData.SetCommonMoneyCount(coinCount.Value);
        _ctx.analitics.SendCommonMoneyCount(_ctx.playersData.GetCommonMoneyCount());
        _ctx.analitics.SendCurrentMoneyCount(coinCount.Value);
        _ctx.analitics.SendRunTime(DateTime.UtcNow.Subtract(_startTime));
    }

    private string AddZeroBeforeValue(int value)
    {
        int count = 5;
        string result = "";
        int length = value.ToString().Length;
        for (int i = 0; i < count - length; i++)
        {
            result += "0";
        }
        result += value.ToString();
        return result;
    }

    private void CreateLife()
    {
        for (int i = 0; i < 6; i++)
        {
            RectTransform rect = Instantiate(oneLifePrefab, lifeContainer).GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 0 - i * 100);
            rect.GetComponent<CanvasGroup>().alpha = 0;
            _lifeList.Add(rect.GetComponent<CanvasGroup>());
        }
    }

    private void RenderHpCount(int count)
    {
        HideAllLife();
        for (int i = 0; i < count; i++)
        {
            _lifeList[i].alpha = 1;
        }
    }

    private void HideAllLife()
    {
        foreach (var item in _lifeList)
        {
            item.alpha = 0;
        }
    }
}
