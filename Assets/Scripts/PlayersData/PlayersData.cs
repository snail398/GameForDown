using UnityEngine;

public class PlayersData 
{
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
}
