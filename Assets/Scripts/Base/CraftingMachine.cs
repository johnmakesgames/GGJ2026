using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

struct CraftingRecipe
{
    public ItemTag ComponentA;
    public ItemTag ComponentB;

    public CraftingRecipe(ItemTag compA, ItemTag compB)
    {
        ComponentA = compA;
        ComponentB = compB;
    }
}

public class CraftingMachine : BaseMachine
{
    private Dictionary<CraftingRecipe, ItemTag> RecipeLookups;

    //Component that handles displaying selected item  
    //Pretty hacky - when inventory scene opens if there is a selector (set on inventory) it changes the inventory to close and sets value on selector
    [SerializeField]
    InventorySelector ItemA;
    [SerializeField]
    InventorySelector ItemB;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RecipeLookups = new Dictionary<CraftingRecipe, ItemTag>();
        RecipeLookups.Add(new CraftingRecipe(ItemTag.ScrapMetal, ItemTag.Gunpowder), ItemTag.Ammo);
        RecipeLookups.Add(new CraftingRecipe(ItemTag.Can, ItemTag.Plant), ItemTag.Food);
        RecipeLookups.Add(new CraftingRecipe(ItemTag.Plant, ItemTag.Slug), ItemTag.Medkit);
        RecipeLookups.Add(new CraftingRecipe(ItemTag.Medkit, ItemTag.Food), ItemTag.Cure);

        List<string> possibleItemStrings = new List<string>(Enum.GetNames(typeof(ItemTag)));
        //Remove string for ItemTag.Count which should always be at the end.
        possibleItemStrings.RemoveAt(possibleItemStrings.Count - 1);

        ItemA.ClearSelection();
        ItemB.ClearSelection();

        possibleItemStrings.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCraftButtonPressed()
    {
        if (ItemA && ItemB && ItemA.HasValidSelection() && ItemB.HasValidSelection())
        {
            CraftingRecipe recipe = new CraftingRecipe();
            recipe.ComponentA = (ItemTag)ItemA.GetCurrentSelection();
            recipe.ComponentB = (ItemTag)ItemB.GetCurrentSelection();
            UseMachine(recipe);
        }
        else
        {
            Debug.Log("Missing a dropdown box somewhere");
        }
    }


    public override bool UseMachine(object contextObj)
    {
        if (GetNearbyPlayerGameObject() == null)
            return false;

        if (contextObj == null)
            return false;

        CraftingRecipe recipe = (CraftingRecipe)(contextObj);
        CraftingRecipe reversedRecipe = new CraftingRecipe(recipe.ComponentB, recipe.ComponentA);

        ItemTag output = ItemTag.COUNT;
        if(RecipeLookups.TryGetValue(recipe, out output) == false)
        {
            if (RecipeLookups.TryGetValue(reversedRecipe, out output) == false)
            {
                Debug.Log("Recipe not found in list.");
                return false;
            }
            else
            {
                recipe = reversedRecipe;
            }
        }

        Debug.Log("BANG BANG SAW SAW CRAFTING MINING?");

        PlayerInventory inventory = GetNearbyPlayerGameObject().GetComponent<PlayerInventory>();

        if (inventory == null)
            return false;

        if(inventory.HasItem(recipe.ComponentA) && inventory.HasItem(recipe.ComponentB))
        {
            if (!inventory.RemoveItem(recipe.ComponentA))
            {
                Debug.Log("Failed to remove item from inventory.");
                return false;
            }

            if (!inventory.RemoveItem(recipe.ComponentB))
            {
                Debug.Log("Failed to remove item from inventory.");
                return false;
            }

            if (inventory.TryAddItem(output))
            {
                Debug.Log("Added medkit.");
                return true;
            }
            else
            {
                Debug.Log("Failed to add item to inventory.");
                return false;
            }
        }

        return false;
    }
}
