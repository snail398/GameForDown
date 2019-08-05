﻿using UnityEngine;
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

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            playersCoins.Value = _ctx.playersData.GetCoinsCount();
            UpdateCoinsCount();
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