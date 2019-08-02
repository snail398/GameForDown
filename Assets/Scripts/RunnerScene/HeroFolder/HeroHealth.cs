using UnityEngine;

namespace Assets.Scripts.HeroFolder
{
    public class HeroHealth : MonoBehaviour
    {
        [SerializeField] IntVariable HeroLife;
        [SerializeField] GameEvent OnHpSetted;
        [SerializeField] GameEvent OnDeath;
        [SerializeField] int StartLifeCount;
        void Awake()
        {
            HeroLife.Value = StartLifeCount;
            OnHpSetted?.Raise();
        }

        public void CheckDeathState()
        {
            if (HeroLife.Value.Equals(0))
                OnDeath?.Raise();
        }

        public void SetHealth(int health)
        {
            HeroLife.Value = health;
            OnHpSetted?.Raise();
        }
    }
}
