using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    float StartingHealth;

    private float health;
    public float CurrentHealth
    {
        get
        {
            return health;
        }

        set
        {
            if (value == health)
            {
                return;
            }

            if (value < health)
            {
                if (OnDamage != null)
                {
                    OnDamage(health - value);
                }
            }

            if (value > health)
            {
                if (OnHeal != null)
                { 
                    OnHeal();
                }
            }

            health = value;

            if (health < 0)
            {
                if (OnDeath != null)
                {
                    OnDeath();
                }
            }
        }
    }

    public Action OnDeath;
    public Action<float> OnDamage;
    public Action OnHeal;

    public void Start()
    {
        CurrentHealth = StartingHealth;
    }
}
