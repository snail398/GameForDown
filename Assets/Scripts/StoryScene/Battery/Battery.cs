using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Story
{
    public class Battery
    {
        public struct Ctx
        {
            public BatteryView batteryView;
            public PlayersData playersData;
            public uint maxBatteryCharge;
            public double minutesPerCharge;
            public IAPCore iapCore;
        }

        private Ctx _ctx;
        private uint _chargeCount;
        private DateTime _startCharging;
        private TimeSpan _timeToCharge;
        private IDisposable _timeChargerHandler;
        private IDisposable _timeViewHandler;
        private bool _charging = false;
        private TimeSpan _timeSpanOneCharge;

        public Battery(Ctx ctx)
        {
            _ctx = ctx;
            _timeSpanOneCharge = new TimeSpan(0,(int)_ctx.minutesPerCharge,0);
            _ctx.batteryView.OnClickEnergyButton += OnClickEnergyButton;
            _ctx.batteryView.OnHideClickEnergyButton += OnHideEnergyButton;
            _ctx.iapCore.OnSuccessBuyFullEnergy += RestoreFullEnergy;
            _ctx.batteryView.OnTryButRestoreLife += TryBuyRestoreLife;
            Initialize();
        }

        private void TryBuyRestoreLife()
        {
            _ctx.iapCore.BuyFullEnergy();
            _ctx.playersData.ClearLastChargeDate();
            OnHideEnergyButton();
        }

        public void OnClickEnergyButton()
        {
            if (_chargeCount >= _ctx.maxBatteryCharge) return;
            _ctx.batteryView.ShowBatteryPanel();
            _timeViewHandler = Observable.EveryUpdate()
                .Subscribe(_=>_ctx.batteryView.RenderTime(_timeSpanOneCharge.Subtract(_timeToCharge)));
        }

        private void OnHideEnergyButton()
        {
            _timeViewHandler?.Dispose();
            _ctx.batteryView.HideBatteryPanel();
        }

        private void Initialize()
        {
            if (_ctx.playersData.GetLastDateTime() == "")
                StartBatteryInit();
            else
                BatteryInitFromSave();
            if (_ctx.batteryView != null)
                _ctx.batteryView.Render(_chargeCount);
        }

        private void StartBatteryInit()
        {
            _chargeCount = _ctx.maxBatteryCharge;
            SaveBatteryState();
        }

        private void BatteryInitFromSave()
        {
            uint recharchedCount = CountRechargedCharge();
            _chargeCount = (uint)Mathf.Clamp(_ctx.playersData.GetLastChargeCount() + recharchedCount, 0, _ctx.maxBatteryCharge);
            CheckNeedCharge(DateTime.Parse(_ctx.playersData.GetLastDateTime()).AddMinutes(recharchedCount * _ctx.minutesPerCharge));
        }

        private uint CountRechargedCharge()
        {
            DateTime lastTime = DateTime.Parse(_ctx.playersData.GetLastDateTime());
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan subTime = currentTime.Subtract(lastTime);
            if (subTime.TotalMinutes < 0)
                return 0;
            uint recharched = (uint)(subTime.TotalMinutes / _ctx.minutesPerCharge);
            return recharched;
        }

        private void SaveBatteryState()
        {
            if (_ctx.playersData.GetLastChargeDate() == "")
                _ctx.playersData.SetLastChargeDate();
            _ctx.playersData.SetLastChargeCount(_chargeCount);
        }

        public bool TryUseCharge()
        {
            if (_chargeCount > 0)
            {
                _chargeCount--;
                CheckNeedCharge(DateTime.UtcNow);
                _ctx.batteryView.UseCharge();
                _ctx.batteryView.Render(_chargeCount);
                SaveBatteryState();
                return true;
            }
            return false;
        }

        private void CheckNeedCharge(DateTime startTime)
        {
            if (_chargeCount < _ctx.maxBatteryCharge && !_charging)
            {
                _startCharging = startTime;
                _timeChargerHandler = Observable.EveryUpdate()
                    .Subscribe(_ => StartCharging());
            }
            if (_chargeCount >= _ctx.maxBatteryCharge)
            {
                _ctx.playersData.ClearLastChargeDate();
                OnHideEnergyButton();
            }
        }

        private void StartCharging()
        {
            _charging = true;
            _timeToCharge = DateTime.UtcNow.Subtract(_startCharging);
            if (_timeToCharge.TotalMinutes >= _ctx.minutesPerCharge)
            {
                _chargeCount++;
                _ctx.batteryView.Render(_chargeCount);
                _timeChargerHandler?.Dispose();
                _charging = false;
                CheckNeedCharge(DateTime.UtcNow);
            }
        }

        public void RestoreFullEnergy()
        {
            _chargeCount = _ctx.maxBatteryCharge;
            _ctx.batteryView.Render(_chargeCount);
        }

        public void Dispose()
        {
            _ctx.iapCore.OnSuccessBuyFullEnergy -= RestoreFullEnergy;
            _timeChargerHandler?.Dispose();
            _timeViewHandler?.Dispose();
        }
    }
}
