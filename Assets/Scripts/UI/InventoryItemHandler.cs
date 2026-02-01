using Unity.VisualScripting;
using UnityEngine;

public class InventoryItemHandler : MonoBehaviour
{
    private InventoryUIController InventoryController;


    ItemTag m_Tag;
    int m_ItemId = 0;

    private void Awake()
    {
        InventoryController = GameObject.FindFirstObjectByType<InventoryUIController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Configure(ItemTag tag, int i)
    {
        m_Tag = tag;
        m_ItemId = i;
    }

    public void OnClick()
    {
        InventoryController.Selected(this);
    }
}
