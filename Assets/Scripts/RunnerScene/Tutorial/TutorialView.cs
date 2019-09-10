using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tutorial
{
    public class TutorialView : MonoBehaviour
    {
        [SerializeField] private Image arrow;
        [SerializeField] private Text text;
        [SerializeField] private Image panel;
        [SerializeField] private Image changeColor;
        [SerializeField] private Image changeShape;

        private float time;
        private bool horizontal;
        private IDisposable _timerDisposable;
        private IDisposable _arrowDisposable;
        private Vector2 _defaultPanelSize;

        private void Awake()
        {
            _defaultPanelSize = panel.rectTransform.sizeDelta;
        }
        public void ShowArrow(string direction)
        {
            _arrowDisposable?.Dispose();
            _arrowDisposable = Observable.Timer(System.TimeSpan.FromSeconds(3))
                .Subscribe(_ => HideArrow()).AddTo(this);
            arrow.enabled = true;
            float zAngle = 0;
            switch (direction)
            {
                case "up":
                    zAngle = 90;
                    horizontal = false;
                    break;
                case "down":
                    zAngle = 270;
                    horizontal = false;
                    break;
                case "left":
                    zAngle = 180;
                    horizontal = true;
                    break;
                case "right":
                    zAngle = 0;
                    horizontal = true;
                    break;
                default:
                    break;
            }
            arrow.transform.rotation = Quaternion.Euler(0,0,zAngle);
        }

        public void HideArrow()
        {
            arrow.enabled = false;
        }

        public void ShowChangeColor()
        {
            changeColor.gameObject.SetActive(true);
            panel.rectTransform.sizeDelta += new Vector2(0, 75);
        }

        public void ShowChangeShape()
        {
            changeShape.gameObject.SetActive(true);
            panel.rectTransform.sizeDelta += new Vector2(0, 75);
        }

        public void ShowGoodLuck()
        {
            panel.rectTransform.sizeDelta -= new Vector2(100, 0);
        }

        public void ShowText(string message)
        {
            panel.rectTransform.sizeDelta = _defaultPanelSize;
            panel.gameObject.SetActive(true);
            text.enabled = true;
            text.text = message;
            _timerDisposable?.Dispose();
            _timerDisposable = Observable.Timer(System.TimeSpan.FromSeconds(3))
                .Subscribe(_ => HideText()).AddTo(this);
        }

        public void HideText()
        {
            panel.gameObject.SetActive(false);
            text.enabled = false;
            if (changeColor.gameObject.activeSelf)
                changeColor.gameObject.SetActive(false);
            if (changeShape.gameObject.activeSelf)
                changeShape.gameObject.SetActive(false);
        }

        void Update()
        {
            time += Time.deltaTime*5;
            arrow.transform.position = horizontal ? new Vector2(arrow.transform.position.x+Mathf.Cos(time)*3,arrow.transform.position.y) : new Vector2(arrow.transform.position.x , arrow.transform.position.y + Mathf.Cos(time) * 3);
        }
    }
}
