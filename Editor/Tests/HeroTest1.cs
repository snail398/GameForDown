using System;
using NUnit.Framework;

namespace Editor.Tests
{
    public class HeroTest1 
    {
        public class CreatingHero
        {
            [Test]
            public void _0_Hero_Has_Health()
            {
                Hero mainHero = new Hero();
                Assert.AreEqual(0, mainHero.CurrentHealth);
            }
        }

        public class Hero
        {
            public Hero()
            {
            }

            public Hero(int currentHealth, int maxHeath)
            {
                if (currentHealth > maxHeath) throw new IndexOutOfRangeException("Current Health cannot be more than Max health");
                this.CurrentHealth = currentHealth;
                this.MaxHeath = maxHeath;
            }

            public int CurrentHealth { get; internal set; }

            public int MaxHeath { get; internal set; }
        }
    }
}
