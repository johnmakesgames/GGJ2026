using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float health;
    public float CurrentHealth
    {
        get
        {
            return health;
        }

        set
        {
            if (value < health)
            {
                OnDamage();
            }

            if (value > health)
            {
                OnHeal();
            }

            health = value;
            
            if (health < 0)
            {
                OnDeath();
            }
        }
    }

    Action OnDeath;
    Action OnDamage;
    Action OnHeal;
}
