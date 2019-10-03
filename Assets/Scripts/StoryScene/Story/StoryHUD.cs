using System;
using UniRx;

namespace Story
{
    public class StoryHUD
    {
        public struct Ctx
        {
            public StoryHUDView storyHUDView;
            public PlayersData playersData;
            public TwineStory twineStory;
            public Action reloadGame;
            public Action loadRunScene;
            public ReactiveProperty<bool> needStartRunButton;
            public AnaliticsCore analitics;
            public IAPCore iapCore;
        }

        private Ctx _ctx;
        private Battery _battery;

        public StoryHUD(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.storyHUDView.OnChooseOption += ChooseOption;
            _ctx.storyHUDView.OnClickRunButton += StartRun;
            _ctx.storyHUDView.OnShowAds += WatchAds;
            _ctx.twineStory.ShowRollbackWindow += ShowRollbackWindow;
            _ctx.storyHUDView.OnClickRollbackButton += ClickRollbackButton;
            _ctx.storyHUDView.OnClickToSaveButton += ClickToSaveButton;
        }

        public void InitializeHUD()
        {
            StoryHUDView.Ctx viewCtx = new StoryHUDView.Ctx
            {
                playersData = _ctx.playersData,
                reloadGame = _ctx.reloadGame,
                needStartRunButton = _ctx.needStartRunButton,
            };
            _ctx.storyHUDView.SetCtx(viewCtx);
            if (!_ctx.playersData.CheckNeedPcLoading())
                _ctx.storyHUDView.SetPcOn();
            CreateBattery();
        }

        private void CreateBattery()
        {
            Battery.Ctx batteryCtx = new Battery.Ctx
            {
                maxBatteryCharge = 5,
                minutesPerCharge = 20,
                playersData = _ctx.playersData,
                batteryView = _ctx.storyHUDView.BatteryView,
                iapCore = _ctx.iapCore,
            };
            _battery = new Battery(batteryCtx);
        }

        public void TurnOnPC()
        {
            _ctx.storyHUDView.TurnOnPc();
        }

        private void SaveGame()
        {
            _ctx.playersData.SetCointCount(_ctx.storyHUDView.PlayersCoins.Value);
        }

        private void ShowRollbackWindow()
        {
            _ctx.storyHUDView.ShowRollbackWindow();
        }

        public void ClickRollbackButton()
        {
            _ctx.analitics.SendTryRollback();
            _ctx.storyHUDView.HideRollbackWindow();
            if (CheckHasEnoughCoins(500))
            {
                RemoveCoins(500);
                _ctx.twineStory.RollBack();
                _ctx.analitics.SendRollback();
            }
        }

        public void ClickToSaveButton()
        {
            _ctx.storyHUDView.HideRollbackWindow();
            _ctx.twineStory.ToSave();
        }

        public void StartRun()
        {
            if (_battery.TryUseCharge())
            {
                SaveGame();
                _ctx.loadRunScene?.Invoke();
            }
            else
            {
                _battery.OnClickEnergyButton();
            }
        }

        public void ChooseOption(int option)
        {
            int coinsToOption = _ctx.twineStory.GetCoinsForOption(option);
            if (CheckHasEnoughCoins(coinsToOption))
            {
                _ctx.twineStory.MakeChoice(option);
                RemoveCoins(coinsToOption);
                _ctx.analitics.SendCurrentMoneyCount(_ctx.playersData.GetCoinsCount());
            }
        }

        private bool CheckHasEnoughCoins(int coinsRequired)
        {
            if (coinsRequired <= _ctx.storyHUDView.PlayersCoins.Value) return true;
            else
            {
                _ctx.storyHUDView.ShowNotEnoughCoinsScreen();
                return false;
            }
        }
        private void RemoveCoins(int coinsToRemove)
        {
            _ctx.storyHUDView.PlayersCoins.Value -= coinsToRemove;
            _ctx.storyHUDView.UpdateCoinsCount();
        }
        
        public void WatchAds()
        {
            RemoveCoins(0);
            _ctx.storyHUDView.HideNotEnoughCoinsScreen();
        }

        public void Dispose()
        {
            _battery.Dispose();
        }
    }
}
