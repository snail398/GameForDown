using UnityEngine;

namespace Assets.Scripts.Background
{
    public class BackgroundRepeater : MonoBehaviour
    {
        [SerializeField] private float _startY;
        [SerializeField] private float _endY;

        private float _speed;
        private GameSpeed _infoSource;

        void Awake()
        {
            _speed = 3;
            _infoSource = GameObject.Find("Root").GetComponent<GameSpeed>();
        }

        void Update()
        {
            _speed = _infoSource.ObstacleSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * _speed,transform.position.z);

            if (transform.position.y < _endY) transform.position = new Vector3( transform.position.x,_startY, transform.position.z);
        }
    }
}
