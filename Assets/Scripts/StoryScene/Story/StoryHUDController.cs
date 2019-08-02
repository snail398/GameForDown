using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Story
{
    public class StoryHUDController : MonoBehaviour
    {
        [SerializeField] private Text playersCoinsCountText;

        [SerializeField] private Text storyStepText;

        [SerializeField] private Text firstOptionText;
        [SerializeField] private Text firstOptionCount;

        [SerializeField] private Text secondOptionText;
        [SerializeField] private Text secondOptionCount;

        [SerializeField] private Text thirdOptionText;
        [SerializeField] private Text thirdOptionCount;

        [SerializeField] private Image notEnoughCoinsScreen;

        [SerializeField] private IntVariable playersCoins;
    
        [SerializeField] private int coinsToRun = 0;
        [SerializeField] private int coinsToFirstOption = 10;
        [SerializeField] private int coinsToSecondOption = 20;
        [SerializeField] private int coinsToThirdOption = 30;

        private StoryController storyController;

        void Awake()
        {
            playersCoins.Value = PlayerPrefs.GetInt("coins_count");
            storyController = GetComponent<StoryController>();
            UpdateCoinsCount();
            firstOptionCount.text = coinsToFirstOption.ToString();
            secondOptionCount.text = coinsToSecondOption.ToString();
            thirdOptionCount.text = coinsToThirdOption.ToString();
        }

        public void StartRun()
        {
            if (CheckHasEnoughCoins(coinsToRun))
            {
                SceneManager.LoadScene(1);
                RemoveCoins(coinsToRun);
                SaveGame();
            }
        }

        public void WatchAds()
        {
            RemoveCoins(-10);
            notEnoughCoinsScreen.gameObject.SetActive(false);
        }

        private bool CheckHasEnoughCoins(int coinsRequired)
        {
            if (coinsRequired <= playersCoins.Value) return true;
            else
            {
                //show table for start run
                notEnoughCoinsScreen.gameObject.SetActive(true);
                return false;
            }
        }

        private void RemoveCoins(int coinsToRemove)
        {
            playersCoins.Value -= coinsToRemove;
            UpdateCoinsCount();
        }

        private void UpdateCoinsCount()
        {
            playersCoinsCountText.text = playersCoins.Value.ToString();
        }
        
        public void ChooseOption(int option)
        {
            int coinsToOption = storyController.GetCoinsForOption(option);
            if (CheckHasEnoughCoins(coinsToOption))
            {
                storyController.MakeChoice(option);
                RemoveCoins(coinsToOption);
            }
        }

        void OnApplicationQuit()
        {
            SaveGame();
        }

        private void SaveGame()
        {
            PlayerPrefs.SetInt("coins_count", playersCoins.Value);
        }

        public void RestartGame()
        {
            playersCoins.Value = 0;
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
