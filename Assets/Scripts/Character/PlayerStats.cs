using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    float StartingOxygen;

    [SerializeField]
    float MaximumOxygen;

    [SerializeField]
    float StartingHealth;

    [SerializeField]
    float MaximumHealth;

    private float oxygen;
    public float CurrentOxygen
    {
        get
        {
            return oxygen;
        }
        set
        {
            if (value > MaximumOxygen)
            {
                oxygen = MaximumOxygen;
            }
            else
            {
                oxygen = value;
            }
        }
    }

    Health healthComponent;
    public float CurrentHealth
    {
        get
        {
            return healthComponent.CurrentHealth;
        }
        set
        {
            if (value > MaximumHealth)
            {
                healthComponent.CurrentHealth = MaximumHealth;
            }
            else
            {
                healthComponent.CurrentHealth = value;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthComponent = GetComponent<Health>();

        CurrentOxygen = StartingOxygen;
        CurrentHealth = StartingHealth;
    }

    public void IncreaseMaxHealth(float increaseAmount, bool restoreToFull)
    {
        MaximumHealth += increaseAmount;
        if (restoreToFull)
        {
            CurrentHealth = MaximumHealth;
        }
    }

    public void IncreaseMaxOxygen(float increaseAmount, bool restoreToFull)
    {
        MaximumOxygen += increaseAmount;
        if (restoreToFull)
        {
            CurrentOxygen = MaximumHealth;
        }
    }
}
