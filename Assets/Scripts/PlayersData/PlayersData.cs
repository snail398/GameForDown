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
}
