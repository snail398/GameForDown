using UnityEngine;

namespace Assets.Scripts.Coins
{
    public class Mover : MonoBehaviour
    {
        private float _speed;
        private GameSpeed _speedSource;

        void Awake()
        {
            _speedSource = GameObject.Find("Root").GetComponent<Root>().GetComponent<GameSpeed>();
        }

        void Update()
        {
            _speed = _speedSource.ObstacleSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * _speed, transform.position.z);
        }
    }
}
