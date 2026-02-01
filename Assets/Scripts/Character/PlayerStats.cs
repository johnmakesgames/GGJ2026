using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            if (oxygen == 0 && value > 0)
            {
                DamageOverTimePerSecond = 0;
            }

            if (value > MaximumOxygen)
            {
                oxygen = MaximumOxygen;
            }
            else
            {
                oxygen = value;
            }

            // Once we're out of oxygen start taking our health down instead
            if (oxygen < 0)
            {
                DamageOverTimePerSecond -= value;
                oxygen = 0;
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

            if (value > MaximumHealth)
            {
                healthComponent.CurrentHealth = MaximumHealth;
            }
            else
            {
                TakeDamage(healthComponent.CurrentHealth - value);
                healthComponent.CurrentHealth = value;
            }

            if (healthComponent.CurrentHealth <= 0)
            {
                SceneManager.LoadScene("DeathScene");
            }

            if (playerStatsUI != null)
            {
                playerStatsUI.SetHealthBarValue(CurrentHealth, MaximumHealth);
            }
        }
    }

    WeaponController weaponController;
    public int AmmoRemainingCount
    {
        get
        {
            return weaponController.ammoStockpile;
        }
    }

    public int AmmoInGunCount
    {
        get
        {
            return weaponController.GetAmmoInGun();
        }
    }

    public void AddAmmoToStockpile(int amount)
    {
        weaponController.AddAmmoToStockpile(amount);
    }

    private int peopleCured;

    [SerializeField]
    OxygenBarControl playerStatsUI;

    [SerializeField]
    WorldUIController worldUIController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthComponent = GetComponent<Health>();

        CurrentOxygen = StartingOxygen;
        CurrentHealth = StartingHealth;

        playerStatsUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<OxygenBarControl>();
        worldUIController = GameObject.FindGameObjectWithTag("WorldUI").GetComponent<WorldUIController>();

        peopleCured = 0;
    }

    void Update()
    {
        CurrentOxygen -= OxygenUsagePerSecond * Time.deltaTime;
        CurrentHealth -= DamageOverTimePerSecond * Time.deltaTime;
    }

    void TakeDamage(float dmg)
    {
        if (worldUIController != null && CurrentHealth > 0.0f && dmg != 0)
        {
            bool reuseDamageForSameSource = true;
            worldUIController.ShowDamage(dmg, gameObject.transform.position, this.gameObject, reuseDamageForSameSource);
        }
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

    public void SignalCured()
    {
        peopleCured++;

        if (peopleCured > 5)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
