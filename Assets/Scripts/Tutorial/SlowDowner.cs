using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    public class SlowDowner : MonoBehaviour
    {

        private GameSpeed _gameSpeed;
        private bool needSlowDown = true;
        void Awake()
        {
            _gameSpeed = GameObject.Find("Root").GetComponent<Root>().GetComponent<GameSpeed>();
        }
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                gameObject.SetActive(false);
                if (needSlowDown)
                    _gameSpeed.SlowDownGame();
            }
        }

        public void SetNoSlowDown()
        {
            needSlowDown = false;
        }

        public void SetPosition(Vector3 newPos)
        {
            gameObject.SetActive(true);
            transform.position = newPos;
        }
    }
}
