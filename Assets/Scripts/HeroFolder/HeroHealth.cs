using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (HeroLife.Value.Equals(0)) OnDeath?.Raise();
    }
}
