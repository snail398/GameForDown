using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageHistory 
{
    public struct Ctx
    {
        public PlayersData playersData;
    }

    private string _previousPassage;
    private string _currentPassage;
    private Ctx _ctx;
    private Dictionary<string, string> _storyVars;

    public Dictionary<string, string> StoryVars
    {
        get
        {
            return _storyVars;
        }
        set
        {
            _storyVars = value;
            if (_ctx.playersData != null)
            {
                _ctx.playersData.SetStoryVars(JsonConvert.SerializeObject(_storyVars));
            }
        }
    }

    public PassageHistory(Ctx ctx)
    {
        _ctx = ctx;
        if (_ctx.playersData != null)
        {
            _currentPassage = _ctx.playersData.GetLastPassage();
            _previousPassage = _ctx.playersData.GetPreviousPassage();
            string vars = _ctx.playersData.GetStoryVars();
            if (vars != "")
                _storyVars = JsonConvert.DeserializeObject<Dictionary<string, string>>(vars);
        }
    }

    public void AddToHistory(string newPassage)
    {
        _previousPassage = _currentPassage;
        _currentPassage = newPassage;
        SaveHistory();
    }
    
    public bool TryGetPreviousPassage(out string previousPassage)
    {
        previousPassage = _previousPassage;
        return previousPassage != "";
    }

    public string GetLastPassage()
    {
        string dataPassage = _currentPassage;
        if (dataPassage == "beemoGoToRunTutorial" && !_ctx.playersData.CheckNeedRunTutorial())
            dataPassage = "beemoTutorialEnd";
        return dataPassage;
    }

    private void SaveHistory()
    {
        if (_ctx.playersData != null)
        {
            _ctx.playersData.SetPreviousPassage(_previousPassage);
            _ctx.playersData.SetLastPassage(_currentPassage);
        }
    }
}
