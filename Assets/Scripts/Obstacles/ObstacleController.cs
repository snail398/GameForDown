using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] protected GameEvent OnHeroDamaged;
        [SerializeField] protected IntVariable HeroLife;

        protected LevelConfig _configSource;
        protected MeshFilter _filter;
        protected Material _material;

        public virtual void Awake()
        {
            _configSource = GameObject.Find("Root").GetComponent<Root>().GetComponent<LevelConfig>();
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
            OnHeroDamaged?.Raise();
            gameObject.GetComponent<PoolObject>().ReturnToPool();
        }
    }
}
