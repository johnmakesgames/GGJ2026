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

    public Action OnDeath;
    public Action OnDamage;
    public Action OnHeal;

    public void Start()
    {
        CurrentHealth = StartingHealth;
    }
}
