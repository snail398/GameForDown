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
                minutesPerCharge = 30,
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
