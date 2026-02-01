using System.Data;
using UnityEngine;

public class PlayerPickupResponse : MonoBehaviour
{
    PlayerStats playerStats;
    WorldUIController worldUIController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        worldUIController = GameObject.FindGameObjectWithTag("WorldUI").GetComponent<WorldUIController>();
    }

    public void PickedUp(PickupTypes pickupType, int pickupAmount, ItemTag item)
    {
        switch (pickupType)
        {
            case PickupTypes.Health:
                playerStats.CurrentHealth += pickupAmount;
                worldUIController?.ShowStatGained($"+{pickupAmount}" + " Health Gained", this.gameObject, WorldUIController.WorldUIType.HealthGame);
                break;
            case PickupTypes.Oxygen:
                playerStats.CurrentOxygen += pickupAmount;
                worldUIController?.ShowStatGained("Oxygen Gained", this.gameObject, WorldUIController.WorldUIType.OxygenGain);
                break;
            case PickupTypes.Ammo:
                playerStats.AddAmmoToStockpile(pickupAmount * 5);
                int ammoGained = pickupAmount * 5;
                worldUIController?.ShowStatGained($"{ammoGained}" + " Ammo", this.gameObject, WorldUIController.WorldUIType.Scavenge);
                break;
            case PickupTypes.FullHeal:
                playerStats.IncreaseMaxHealth(0, true);
                worldUIController?.ShowStatGained("Refilled Health", this.gameObject, WorldUIController.WorldUIType.HealthGame);
                break;
            case PickupTypes.FullOxygen:
                playerStats.IncreaseMaxOxygen(0, true);
                worldUIController?.ShowStatGained("Refilled Oxygen", this.gameObject, WorldUIController.WorldUIType.OxygenGain);
                break;
            case PickupTypes.FullAmmo:
                Debug.LogWarning("No implementation for Full Ammmo pickup");
                break;
            case PickupTypes.IncreaseMaxHealth:
                playerStats.IncreaseMaxHealth(pickupAmount, false);
                worldUIController?.ShowStatGained("Increased Max Health", this.gameObject, WorldUIController.WorldUIType.HealthGame);
                break;
            case PickupTypes.IncreaseMaxOxygen:
                playerStats.IncreaseMaxOxygen(pickupAmount, false);
                worldUIController?.ShowStatGained("Increased Max Oxygen", this.gameObject, WorldUIController.WorldUIType.OxygenGain);
                break;
            case PickupTypes.IncreaseMaxAmmo:
                Debug.LogWarning("No implementation for increase max Ammmo pickup");
                break;
            case PickupTypes.Item:
                if(this.GetComponent<PlayerInventory>().TryAddItem(item))
                {
                    worldUIController?.ShowItemPickedup(item, this.gameObject);
                }
                break;
            default:
                Debug.LogError($"Pickup default case entered for {pickupType.ToString()}");
                break;
        }
    }
}