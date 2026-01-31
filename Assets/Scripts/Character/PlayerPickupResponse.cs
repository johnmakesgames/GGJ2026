using UnityEngine;

public class PlayerPickupResponse : MonoBehaviour
{
    PlayerStats playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void PickedUp(PickupTypes pickupType, int pickupAmount)
    {
        switch (pickupType)
        {
            case PickupTypes.Health:
                playerStats.CurrentHealth += pickupAmount;
                break;
            case PickupTypes.Oxygen:
                playerStats.CurrentOxygen += pickupAmount;
                break;
            case PickupTypes.Ammo:
                Debug.LogWarning("No implementation for Ammmo pickup");
                break;
            case PickupTypes.FullHeal:
                playerStats.IncreaseMaxHealth(0, true);
                break;
            case PickupTypes.FullOxygen:
                playerStats.IncreaseMaxOxygen(0, true);
                break;
            case PickupTypes.FullAmmo:
                Debug.LogWarning("No implementation for Full Ammmo pickup");
                break;
            case PickupTypes.IncreaseMaxHealth:
                playerStats.IncreaseMaxHealth(pickupAmount, false);
                break;
            case PickupTypes.IncreaseMaxOxygen:
                playerStats.IncreaseMaxOxygen(pickupAmount, false);
                break;
            case PickupTypes.IncreaseMaxAmmo:
                Debug.LogWarning("No implementation for increase max Ammmo pickup");
                break;
            default:
                Debug.LogError($"Pickup default case entered for {pickupType.ToString()}");
                break;
        }
    }
}