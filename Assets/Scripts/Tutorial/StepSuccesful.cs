using UniRx;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    public class StepSuccesful : MonoBehaviour
    {
        public ReactiveCommand stepComplite = new ReactiveCommand();

        private float _speed;
        private GameSpeed _speedSource;
        private Root _root;

        void Awake()
        {
            _root = GameObject.Find("Root").GetComponent<Root>();
            _speedSource = _root.GetComponent<GameSpeed>();
        }

        void Update()
        {
            _speed = _speedSource.ObstacleSpeed;
            transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * _speed);
        }
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                gameObject.SetActive(false);
                stepComplite.Execute();
            }
        }

        public void SetPosition(Vector3 newPos)
        {
            gameObject.SetActive(true);
            transform.position = newPos;
        }
    }
}
