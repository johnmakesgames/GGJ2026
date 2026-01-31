using UnityEngine;

public class CraftingMachine : BaseMachine
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void UseMachine()
    {
        if (GetNearbyPlayerGameObject() == null)
            return;

        Debug.Log("BANG BANG SAW SAW CRAFTING MINING?");

        PlayerInventory inventory = GetNearbyPlayerGameObject().GetComponent<PlayerInventory>();

        if (inventory == null)
            return;

        if(inventory.HasItem(ItemTag.ScrapMetal))
        {
            if (!inventory.RemoveItem(ItemTag.ScrapMetal))
            {
                Debug.Log("Failed to remove item from inventory.");
                return;
            }
            
            if(inventory.TryAddItem(ItemTag.Medkit))
            {
                Debug.Log("Added medkit.");
            }
            else
            {
                Debug.Log("Failed to add item to inventory.");
                return;
            }
        }
    }
}
