using System;

namespace Assets.Scripts.HeroFolder
{
    public class HealthChanger
    {
        private readonly Hero _target;
        public HealthChanger(Hero target)
        {
            this._target = target;
        }

        public void Heal(int healedValue)
        {
            if (healedValue < 0) throw new IndexOutOfRangeException("Cannot heal on negative value");
            _target.CurrentHealth = healedValue + _target.CurrentHealth > _target.MaxHeath
                ? _target.MaxHeath
                : _target.CurrentHealth + healedValue;
        }

        public void Damage(int damagedValue)
        {
            if (damagedValue < 0) throw new IndexOutOfRangeException("Cannot damage on negative value");
            _target.CurrentHealth =  _target.CurrentHealth - damagedValue < 0
                ? 0
                : _target.CurrentHealth - damagedValue;
        }
    }
}