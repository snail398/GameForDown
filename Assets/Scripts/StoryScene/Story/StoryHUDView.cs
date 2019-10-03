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
            public PlayersData playersData;
            public Action reloadGame;
            public ReactiveProperty<bool> needStartRunButton;
        }

        [SerializeField] private Text _playersCoinsCountText;
        [SerializeField] private Image _notEnoughCoinsScreen;
        [SerializeField] private IntVariable _playersCoins;
        [SerializeField] private int _coinsToRun = 0;
        [SerializeField] private Image _computerScreenWrapper;
        [SerializeField] private Image _reallyComputerScreen;
        [SerializeField] private Image _coinScreen;
        [SerializeField] private Button _startRunButton;
        [SerializeField] private BatteryView _batteryView;
        [SerializeField] private Image _rollbackWindow;

        private Ctx _ctx;
        private Animator _screenAnimator;

        private Animator _notEnoughCoinsAnimator;
        private CanvasGroup _notEnoughCoinsCanvasGroup;

        public event Action OnClickRunButton;
        public event Action OnClickRollbackButton;
        public event Action OnClickToSaveButton;
        public event Action<int> OnChooseOption;
        public event Action OnShowAds;

        public BatteryView BatteryView => _batteryView;
        public IntVariable PlayersCoins => _playersCoins;

        private void Awake()
        {
            ResizePC();
            _screenAnimator = _computerScreenWrapper.GetComponent<Animator>();
            _startRunButton.onClick.AddListener(StartRun);
            _notEnoughCoinsAnimator = _notEnoughCoinsScreen.GetComponentInChildren<Animator>();
            _notEnoughCoinsCanvasGroup = _notEnoughCoinsScreen.GetComponent<CanvasGroup>();

            _notEnoughCoinsCanvasGroup.alpha = 0;
            _notEnoughCoinsCanvasGroup.blocksRaycasts = false;
        }

        private void ResizePC()
        {
            switch (Screen.height / Screen.width)
            {
                case 1920 / 1080:
                    SetScreenOffset(45f, 6.5f);
                    break;
                case 2160 / 1080:
                    SetScreenOffset(55f, 8f);
                    break;
            }
        }

        public void ShowRollbackWindow()
        {
            _rollbackWindow.gameObject.SetActive(true);
        }

        public void HideRollbackWindow()
        {
            _rollbackWindow.gameObject.SetActive(false);
        }

        public void ClickRollbackButton()
        {
            OnClickRollbackButton?.Invoke();
        }

        public void ClickToSaveButton()
        {
            OnClickToSaveButton?.Invoke();
        }

        private void SetScreenOffset(float top, float bottom)
        {
            RectTransform chargeContainer = _batteryView.GetComponent<RectTransform>();
            _reallyComputerScreen.rectTransform.offsetMax = new Vector2(_reallyComputerScreen.rectTransform.offsetMax.x, -top);
            _reallyComputerScreen.rectTransform.offsetMin = new Vector2(_reallyComputerScreen.rectTransform.offsetMin.x, bottom);
            _coinScreen.rectTransform.sizeDelta = new Vector2(_coinScreen.rectTransform.sizeDelta.x, top);
            chargeContainer.sizeDelta = new Vector2(chargeContainer.sizeDelta.x, top);
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
            if (_screenAnimator != null)
                _screenAnimator.enabled = false;
            _computerScreenWrapper.color = Color.white;
            _coinScreen.GetComponent<CanvasGroup>().alpha = 1;
            _batteryView.SetChargeVisible();
        }

        public void TurnOnPc()
        {
            if (_screenAnimator != null)
            {
                _screenAnimator.enabled = true;
                _screenAnimator.SetTrigger("PcTurnOn");
            }
        }

        public void StartRun()
        {
            OnClickRunButton?.Invoke();
        }


        public void WatchAds()
        {
            OnShowAds?.Invoke();
        }
        
        public void ShowNotEnoughCoinsScreen()
        {
            _notEnoughCoinsAnimator.SetTrigger("ShowPanel");
            _notEnoughCoinsCanvasGroup.alpha = 1;
            _notEnoughCoinsCanvasGroup.blocksRaycasts = true;
        }

        public void HideNotEnoughCoinsScreen()
        {
            _notEnoughCoinsAnimator.SetTrigger("HidePanel");
            _notEnoughCoinsCanvasGroup.blocksRaycasts = false;
        }

        public void UpdateCoinsCount()
        {
            if (_playersCoinsCountText != null)
                _playersCoinsCountText.text = _playersCoins.Value.ToString();
        }
        
        public void ChooseOption(int option)
        {
            OnChooseOption?.Invoke(option);
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
