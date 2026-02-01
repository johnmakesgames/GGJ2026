using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PickupComponent : MonoBehaviour
{
    [SerializeField]
    PickupTypes pickupType;

    [SerializeField]
    int pickupAmount = 1;

    [SerializeField, Description("If the pickup type is set to item, this determines which item.")]
    ItemTag pickupItem = ItemTag.COUNT;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPickupResponse response = other.gameObject.GetComponent<PlayerPickupResponse>();
        if (response != null)
        {
            response.PickedUp(pickupType, pickupAmount, pickupItem);
            Destroy(this.gameObject);
        }
    }
}
