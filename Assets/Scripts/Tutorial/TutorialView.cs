using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tutorial
{
    public class TutorialView : MonoBehaviour
    {
        [SerializeField] private Image arrow;
        [SerializeField] private Text text;

        private float time;
        private bool horizontal;

        public void ShowArrow(string direction)
        {
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

        public void ShowText(string message)
        {
            text.enabled = true;
            text.text = message;
            Observable
                .Timer(System.TimeSpan.FromSeconds(3))
                .Subscribe(_ => HideText()).AddTo(this);
        }

        public void HideText()
        {
            text.enabled = false;
        }

        void Update()
        {
            time += Time.deltaTime*5;
            arrow.transform.position = horizontal ? new Vector2(arrow.transform.position.x+Mathf.Cos(time)*3,arrow.transform.position.y) : new Vector2(arrow.transform.position.x , arrow.transform.position.y + Mathf.Cos(time) * 3);
        }
    }
}
