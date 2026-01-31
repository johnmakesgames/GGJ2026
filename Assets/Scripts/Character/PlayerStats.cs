using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    float StartingOxygen;

    [SerializeField]
    float MaximumOxygen;

    [SerializeField]
    public float OxygenUsagePerSecond;

    [SerializeField]
    float StartingHealth;

    [SerializeField]
    float MaximumHealth;

    [SerializeField]
    public float DamageOverTimePerSecond;

    private float oxygen;
    public float CurrentOxygen
    {
        get
        {
            return oxygen;
        }
        set
        {
            if (value == oxygen)
            {
                return;
            }

            if (value > MaximumOxygen)
            {
                oxygen = MaximumOxygen;
            }
            else
            {
                oxygen = value;
            }

            if (playerStatsUI != null)
            {
                playerStatsUI.SetOxygenBarValue(CurrentOxygen, MaximumOxygen);
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
            if (value == CurrentHealth)
            {
                return;
            }

            Debug.Log("Set player health");
            if (value > MaximumHealth)
            {
                healthComponent.CurrentHealth = MaximumHealth;
            }
            else
            {
                healthComponent.CurrentHealth = value;
            }

            if (playerStatsUI != null)
            {
                playerStatsUI.SetHealthBarValue(CurrentHealth, MaximumHealth);
            }
        }
    }


    [SerializeField]
    OxygenBarControl playerStatsUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthComponent = GetComponent<Health>();

        CurrentOxygen = StartingOxygen;
        CurrentHealth = StartingHealth;

        playerStatsUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<OxygenBarControl>();
    }

    void Update()
    {
        CurrentOxygen -= OxygenUsagePerSecond;
        CurrentHealth -= DamageOverTimePerSecond * Time.deltaTime;
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
