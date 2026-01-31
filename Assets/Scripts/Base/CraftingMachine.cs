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
    [SerializeField]
    TMPro.TMP_Dropdown[] UISelectionDropdowns;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RecipeLookups = new Dictionary<CraftingRecipe, ItemTag>();
        RecipeLookups.Add(new CraftingRecipe(ItemTag.ScrapMetal, ItemTag.Gunpowder), ItemTag.Ammo);
        RecipeLookups.Add(new CraftingRecipe(ItemTag.Can, ItemTag.Plant), ItemTag.Food);
        RecipeLookups.Add(new CraftingRecipe(ItemTag.Plant, ItemTag.Slug), ItemTag.Medkit);

        List<string> possibleItemStrings = new List<string>(Enum.GetNames(typeof(ItemTag)));
        //Remove string for ItemTag.Count which should always be at the end.
        possibleItemStrings.RemoveAt(possibleItemStrings.Count - 1);

        for (int i = 0; i < UISelectionDropdowns.Length; i++)
        {
            UISelectionDropdowns[i].ClearOptions();
            UISelectionDropdowns[i].AddOptions(possibleItemStrings);
        }

        possibleItemStrings.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCraftButtonPressed()
    {
        if (UISelectionDropdowns.Length >= 2)
        {
            CraftingRecipe recipe = new CraftingRecipe();
            recipe.ComponentA = (ItemTag)UISelectionDropdowns[0].value;
            recipe.ComponentB = (ItemTag)UISelectionDropdowns[1].value;
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
