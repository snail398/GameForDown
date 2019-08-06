using UnityEngine;
using UnityEngine.UI;
using System;

namespace Story
{
    public class StoryHUDView : MonoBehaviour
    {
        public struct Ctx
        {
            public TwineStory twineStory;
            public PlayersData playersData;
            public Action reloadGame;
            public Action loadRunScene;
        }

        [SerializeField] private Text playersCoinsCountText;
        [SerializeField] private Image notEnoughCoinsScreen;
        [SerializeField] private IntVariable playersCoins;
        [SerializeField] private int coinsToRun = 0;
        [SerializeField] private Image computerScreen;

        private Ctx _ctx;
        private Animator _animator;


        private void Awake()
        {
            _animator = computerScreen.GetComponent<Animator>();
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            playersCoins.Value = _ctx.playersData.GetCoinsCount();
            UpdateCoinsCount();
        }

        public void SetPcOn()
        {
            if (_animator != null)
                _animator.enabled = false;
            computerScreen.color = new Color(0.2385f,0.23585f,0.23585f);
        }

        public void TurnOnPc()
        {
            if (_animator != null)
            {
                _animator.enabled = true;
                _animator.SetTrigger("PcTurnOn");
            }
        }

        public void StartRun()
        {
            if (CheckHasEnoughCoins(coinsToRun))
            {
                _ctx.loadRunScene?.Invoke();
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

        public void UpdateCoinsCount()
        {
            if (playersCoinsCountText != null)
                playersCoinsCountText.text = playersCoins.Value.ToString();
        }
        
        public void ChooseOption(int option)
        {
            int coinsToOption = _ctx.twineStory.GetCoinsForOption(option);
            if (CheckHasEnoughCoins(coinsToOption))
            {
                _ctx.twineStory.MakeChoice(option);
                RemoveCoins(coinsToOption);
            }
        }

        void OnApplicationQuit()
        {
            SaveGame();
        }

        private void SaveGame()
        {
            _ctx.playersData.SetCointCount(playersCoins.Value);
        }

        public void RestartGame()
        {
            playersCoins.Value = 0;
            _ctx.playersData.ClearData();
            _ctx.reloadGame?.Invoke();
        }
    }
}
