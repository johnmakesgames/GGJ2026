using Unity.VisualScripting;
using UnityEngine;

public class InventoryItemHandler : MonoBehaviour
{
    private InventoryUIController InventoryController;
    private CanvasGroup ItemCanvasGroup;


    public ItemTag m_Tag;
    private int m_ItemId = 0;

    private bool Valid = true;

    private void Awake()
    {
        InventoryController = GameObject.FindFirstObjectByType<InventoryUIController>();
        ItemCanvasGroup = GameObject.FindFirstObjectByType<CanvasGroup>();
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
        if (Valid)
        {
            InventoryController.Selected(this);
        }
    }

    public bool IsViableItem()
    {
        return Valid;
    }

    public void Hide()
    {
        if(ItemCanvasGroup)
        {
            ItemCanvasGroup.alpha = 0;
        }
        Valid = false;
    }
}
