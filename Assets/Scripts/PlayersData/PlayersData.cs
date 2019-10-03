using UnityEngine;

public class PlayersData 
{
    public PlayersData()
    {
        SetNeedPcLoading(1);
    }

    public bool CheckNewGameStatus()
    {
        return PlayerPrefs.GetInt("continue_game").Equals(0);
    }

    public void SetGameAlreadyStarted()
    {
        PlayerPrefs.SetInt("continue_game", 1);
    }

    public string GetLastPassage()
    {
        return PlayerPrefs.GetString("last_passage");
    }

    public void SetLastPassage(string lastPassage)
    {
        PlayerPrefs.SetString("last_passage", lastPassage);
    }

    public int GetCoinsCount()
    {
        //       return PlayerPrefs.GetInt("coins_count");
        return 1000;
    }

    public void SetCointCount(int coins)
    {
        PlayerPrefs.SetInt("coins_count", coins);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool CheckNeedPcLoading()
    {
        return PlayerPrefs.GetInt("need_pc_load").Equals(1);
    }

    public void SetNeedPcLoading(int value)
    {
        PlayerPrefs.SetInt("need_pc_load", value);
    }

    public bool CheckNeedRunTutorial()
    {
        return PlayerPrefs.GetInt("tutorial_passed").Equals(0);
    }

    public void SetRunTutorialPassed()
    {
        PlayerPrefs.SetInt("tutorial_passed", 1);
    }

    public string GetLastDateTime()
    {
        return PlayerPrefs.GetString("last_date_time");
    }

    public int GetLastChargeCount()
    {
        return PlayerPrefs.GetInt("last_charge_count");
    }

    public void SetLastChargeCount(uint count)
    {
        PlayerPrefs.SetInt("last_charge_count", (int)count);
    }

    public void SetLastChargeDate()
    {
        PlayerPrefs.SetString("last_date_time", System.DateTime.UtcNow.ToString());
    }

    public string GetLastChargeDate()
    {
        return PlayerPrefs.GetString("last_date_time");
    }

    public void ClearLastChargeDate()
    {
        PlayerPrefs.DeleteKey("last_date_time");
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("max_score");
    }
    public void SetMaxScore(int score)
    {
        PlayerPrefs.SetInt("max_score", score);
    }

    public void SetCommonMoneyCount(int count)
    {
        PlayerPrefs.SetInt("common_money_count", GetCommonMoneyCount() + count);
    }
    public int GetCommonMoneyCount()
    {
        return PlayerPrefs.GetInt("common_money_count", 0);
    }

    public void SetPreviousPassage(string previousPassage)
    {
        PlayerPrefs.SetString("previous_passage", previousPassage);
    }
    public string GetPreviousPassage()
    {
        return PlayerPrefs.GetString("previous_passage");
    }

    public void SetStoryVars(string vars)
    {
        PlayerPrefs.SetString("story_vars", vars);
    }

    public string GetStoryVars()
    {
        return PlayerPrefs.GetString("story_vars");
    }
}
