using Unity.VisualScripting;
using UnityEngine;

public class PickupComponent : MonoBehaviour
{
    [SerializeField]
    PickupTypes pickupType;

    [SerializeField]
    int pickupAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger");
        PlayerPickupResponse response = other.gameObject.GetComponent<PlayerPickupResponse>();
        if (response != null)
        {
            Debug.Log("Is Player");
            response.PickedUp(pickupType, pickupAmount);
            Destroy(this.gameObject);
        }
    }
}
