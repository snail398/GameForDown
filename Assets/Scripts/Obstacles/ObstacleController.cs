using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] protected GameEvent OnHeroDamaged;
        [SerializeField] protected IntVariable HeroLife;

        private float _speed;
        private GameSpeed _speedSource;
        private Root _root;
        protected LevelConfig _configSource;
        protected MeshFilter _filter;
        protected Material _material;

        public virtual void Awake()
        {
            _speed = 3;
            _root = GameObject.Find("Root").GetComponent<Root>();
            _speedSource = _root.GetComponent<GameSpeed>();
            _configSource = _root.GetComponent<LevelConfig>();
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
                HeroDamage();
            }
        }

        protected void HeroDamage()
        {
            // нанести урон герою
            HeroLife.Value--;
            OnHeroDamaged.Raise();
            gameObject.GetComponent<PoolObject>().ReturnToPool();
        }
    }
}
