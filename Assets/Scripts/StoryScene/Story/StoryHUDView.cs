using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

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
            public ReactiveProperty<bool> needStartRunButton;
        }

        [SerializeField] private Text _playersCoinsCountText;
        [SerializeField] private Image _notEnoughCoinsScreen;
        [SerializeField] private IntVariable _playersCoins;
        [SerializeField] private int _coinsToRun = 0;
        [SerializeField] private Image _computerScreen;
        [SerializeField] private Button _startRunButton;

        private Ctx _ctx;
        private Animator _animator;


        private void Awake()
        {
            _animator = _computerScreen.GetComponent<Animator>();
            _startRunButton.onClick.AddListener(StartRun);
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _playersCoins.Value = _ctx.playersData.GetCoinsCount();
            UpdateCoinsCount();
            _ctx.needStartRunButton?.Subscribe(SetStartRunButtonStatus).AddTo(this);
        }

        public void SetStartRunButtonStatus(bool status)
        {
            _startRunButton.gameObject.SetActive(status);
        }

        public void SetPcOn()
        {
            if (_animator != null)
                _animator.enabled = false;
            _computerScreen.color = new Color(0.2385f,0.23585f,0.23585f);
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
            if (CheckHasEnoughCoins(_coinsToRun))
            {
                _ctx.loadRunScene?.Invoke();
                RemoveCoins(_coinsToRun);
                SaveGame();
            }
        }

        public void WatchAds()
        {
            RemoveCoins(-10);
            _notEnoughCoinsScreen.gameObject.SetActive(false);
        }

        private bool CheckHasEnoughCoins(int coinsRequired)
        {
            if (coinsRequired <= _playersCoins.Value) return true;
            else
            {
                //show table for start run
                _notEnoughCoinsScreen.gameObject.SetActive(true);
                return false;
            }
        }

        private void RemoveCoins(int coinsToRemove)
        {
            _playersCoins.Value -= coinsToRemove;
            UpdateCoinsCount();
        }

        public void UpdateCoinsCount()
        {
            if (_playersCoinsCountText != null)
                _playersCoinsCountText.text = _playersCoins.Value.ToString();
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
            _ctx.playersData.SetCointCount(_playersCoins.Value);
        }

        public void RestartGame()
        {
            _playersCoins.Value = 0;
            _ctx.playersData.ClearData();
            _ctx.reloadGame?.Invoke();
        }
    }
}
