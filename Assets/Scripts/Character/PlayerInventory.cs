using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemTag
{
    ExtraOxygenTank,
    Can,
    Plant,
    Gunpowder,
    Food,
    ScrapMetal,
    Medkit,
    Ammo,
    Slug,
    Cure,
    COUNT
}

public class PlayerInventory : MonoBehaviour
{
    Dictionary<ItemTag, int> ItemTagToSlotsLookup = new Dictionary<ItemTag, int>()
    {
        { ItemTag.ExtraOxygenTank, 10 },
        { ItemTag.Can, 1 },
        { ItemTag.Plant, 1 },
        { ItemTag.Gunpowder, 1 },
        { ItemTag.Food, 2 },
        { ItemTag.ScrapMetal, 1 },
        { ItemTag.Slug, 1 },
        { ItemTag.Medkit, 5 },
        { ItemTag.Ammo, 0 },
        { ItemTag.Cure, 2 },
        { ItemTag.COUNT, 0 },
    };

    [SerializeField]
    private List<ItemTag> items;

    [SerializeField]
    public int MaximumInventorySlots;

    PlayerStats playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public List<ItemTag> GetAllInventoryItems()
    {
        return items;
    }

    public bool HasItem(ItemTag itemTag)
    {
        return items.Where(x => x == itemTag).Any();
    }

    public int GetCountOfItem(ItemTag itemTag)
    {
        return items.Where(x => x == itemTag).Count();
    }

    public int GetTotalSlotsRemaining()
    {
        int total = 0;

        foreach (ItemTag item in items)
        {
            total += ItemTagToSlotsLookup[item];
        }

        return total;
    }

    public bool HasRemainingSpaces()
    {
        if (GetTotalSlotsRemaining() < MaximumInventorySlots)
        {
            return true;
        }

        return false;
    }

    public bool HasRoomForItem(ItemTag item)
    {
        if (GetTotalSlotsRemaining() + ItemTagToSlotsLookup[item] <= MaximumInventorySlots)
        {
            return true;
        }

        return false;
    }

    public bool TryAddItem(ItemTag item)
    {
        if (!HasRoomForItem(item))
        {
            return false;
        }

        items.Add(item);
        return true;
    }

    public bool RemoveItem(ItemTag item)
    {
        if (!items.Remove(item))
        {
            Debug.LogError("ATTEMPTED TO REMOVE ITEM THE LIST DOES NOT CONTAIN");
            return false;
        }

        return true;
    }

    public bool RemoveItems(List<ItemTag> items)
    {
        return RemoveItems(items.ToArray());
    }

    public bool RemoveItems(ItemTag[] items)
    {
        bool success = true;

        for (int i = 0; i < items.Length; ++i)
        {
            success &= RemoveItem(items[i]);
        }

        return success;
    }

    public void UseItem(ItemTag item)
    {
        if (playerStats)
        {
            switch (item)
            {
                case ItemTag.ExtraOxygenTank:
                    if (HasItem(item))
                    {
                        playerStats.CurrentOxygen += 50;
                        RemoveItem(item);
                    }
                    break;
                case ItemTag.Medkit:
                    if (HasItem(item))
                    {
                        playerStats.CurrentHealth += 50;
                        RemoveItem(item);
                    }
                    break;
                case ItemTag.Cure:
                    if (HasItem(item))
                    {
                        GameObject curedGuy = GameObject.FindGameObjectsWithTag("Enemy").ToList().OrderBy(x => (x.transform.position - this.transform.position).magnitude).First();
                        
                        if (curedGuy != null)
                        {
                            curedGuy.GetComponent<EnemyController2D>().Cure();
                            RemoveItem(item);
                        }
                    }
                    break;
                case ItemTag.Can:
                case ItemTag.Gunpowder:
                case ItemTag.Food:
                case ItemTag.ScrapMetal:
                case ItemTag.COUNT:
                default:
                    Debug.Log("Attempted to use non-usable item");
                    break;
            }
        }
    }

    private InventorySelector m_InventorySelection;

    public void AddInventorySelectionListener(InventorySelector selector)
    {
        m_InventorySelection = selector;
    }

    public InventorySelector TryConsumeInventorySelection()
    {
        InventorySelector selection = m_InventorySelection;
        m_InventorySelection = null;
        return selection;
    }
}
