using UnityEngine;

public class CrateInventory : PlayerInventory
{
    [SerializeField]
    bool spawnsWithRandomItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(spawnsWithRandomItems && MaximumInventorySlots > 5)
        {
            int maxItems = (int)(MaximumInventorySlots / 5);

            for (int i = 0; i < maxItems; i++)
            {
                ItemTag item = (ItemTag)Random.Range(0, (int)ItemTag.COUNT);
                TryAddItem(item);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
