using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventorySelector : MonoBehaviour
{
    PlayerInventory inventoryRef;

    public Sprite[] InventoryItemUIVisuals;
    public Sprite NoItemSelected;

    ItemTag Selection = ItemTag.COUNT;
    GameObject Child;

    public GameObject m_ButtonObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventoryToMakeSelection()
    {
        //find InventoryOpener and make it do the work
        var openSesame = GameObject.FindAnyObjectByType<OpenInventory>();
        if (openSesame)
        {
            inventoryRef.AddInventorySelectionListener(this); //Auto removed when inventory uses the selection (even if no selection)
            openSesame.OpenInventory_Click();
        }
    }

    public bool HasValidSelection()
    {
        return Selection != ItemTag.COUNT;
    }

    public ItemTag GetCurrentSelection()
    {
        return Selection;
    }

    public void SetItemToShow(ItemTag item)
    {
        Selection = item;
        RefreshVisual();
    }

    public void ClearSelection()
    {
        Selection = ItemTag.COUNT;
        SetSprite(Selection);
    }

    private void RefreshVisual()
    {
        //Instantiate based on value
        if(Selection != ItemTag.COUNT && InventoryItemUIVisuals.Length > (int)Selection)
        {
            SetSprite(Selection);
        }
    }

    void SetSprite(ItemTag tag)
    {
        var image = m_ButtonObject.GetComponent<UnityEngine.UI.Image>();
        image.sprite = tag != ItemTag.COUNT ? InventoryItemUIVisuals[(int)Selection] : NoItemSelected;
    }
}
