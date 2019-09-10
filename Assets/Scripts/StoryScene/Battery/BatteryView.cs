using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class BatteryView : MonoBehaviour
    {
        [SerializeField] private Image _oneChargePrefab;
        [SerializeField] private RectTransform _chargeContainer;
        [SerializeField] private Image _notEnoughEnergyScreen;
        [SerializeField] private Text _timeToNewCharge;
        private readonly List<CanvasGroup> _chargesList = new List<CanvasGroup>();

        public event Action OnClickEnergyButton;
        public event Action OnHideClickEnergyButton;
        public event Action OnTryButRestoreLife;

        private void Awake()
        {
            CreateAllCharge();
        }

        private void CreateAllCharge()
        {
            for (int i = 0; i < 5; i++)
            {
                RectTransform rect = Instantiate(_oneChargePrefab, _chargeContainer).GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0 + i * 13, 0);
                rect.GetComponent<CanvasGroup>().alpha = 0;
                _chargesList.Add(rect.GetComponent<CanvasGroup>());
            }
        }

        public void SetChargeVisible()
        {
            GetComponent<CanvasGroup>().alpha = 1;
        }

        public void ShowBatteryPanel()
        {
            if (!_notEnoughEnergyScreen.gameObject.activeSelf)
                _notEnoughEnergyScreen.gameObject.SetActive(true);
        }

        public void HideBatteryPanel()
        {
            if (_notEnoughEnergyScreen.gameObject.activeSelf)
                _notEnoughEnergyScreen.gameObject.SetActive(false);
        }

        public void UseCharge()
        {

        }

        public void RenderTime(TimeSpan time)
        {
            _timeToNewCharge.text = time.ToString(@"hh\:mm\:ss");
        }

        public void ClickChargePanel()
        {
            OnClickEnergyButton?.Invoke();
        }

        public void HideChargePanel()
        {
            OnHideClickEnergyButton?.Invoke();
        }

        public void TryBuyRestoreLife()
        {
            OnTryButRestoreLife?.Invoke();
        }

        public void Render(uint chargeCount)
        {
            HideAllCharges();
            for (int i = 0; i < chargeCount; i++)
                _chargesList[i].alpha = 1;
        }

        private void HideAllCharges()
        {
            foreach (var item in _chargesList)
                item.alpha = 0;
        }
    }
}
