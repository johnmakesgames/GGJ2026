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
                Debug.Log("Took damage");

                if (OnDamage != null)
                {
                    OnDamage();
                }
            }

            if (value > health)
            {
                Debug.Log("Healed");

                if (OnHeal != null)
                { 
                    OnHeal();
                }
            }

            health = value;
            Debug.Log($"Health now {health}");

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
    public Action OnDamage;
    public Action OnHeal;

    public void Start()
    {
        CurrentHealth = StartingHealth;
    }
}
