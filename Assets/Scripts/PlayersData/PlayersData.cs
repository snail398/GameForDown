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
        return PlayerPrefs.GetInt("coins_count");
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
}
