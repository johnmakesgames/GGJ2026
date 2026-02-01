using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class InventoryUIController : MonoBehaviour
{
    InputAction OpenInventoryAction;

    public GameObject[] InventoryItemUIPrefab;

    PlayerInventory PlayerInventory;

    InventoryItemHandler m_CurrentItem = null;
    InventorySelector m_InventorySelector = null;

    public GameObject VisualIndicator = null;
    public GameObject ConsumeItem = null;
    public GameObject TrashItem = null;

    bool Closing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenInventoryAction = InputSystem.actions.FindAction("Inventory");
        OpenInventoryAction.Enable();

        PlayerInventory = GameObject.FindAnyObjectByType<PlayerInventory>();
        if (PlayerInventory)
        {
            var items = PlayerInventory.GetAllInventoryItems();
            //int i = 0;
            //foreach (var item in items)
            //{
            //    if ((int)item < InventoryItemUIPrefab.Length)
            //    {
            //        GameObject c = Object.Instantiate(InventoryItemUIPrefab[(int)item], this.gameObject.transform);
            //        var handler = c.GetComponent<InventoryItemHandler>();
            //        if (handler != null)
            //        {
            //            handler.Configure(item, i++);
            //        }
            //    }
            //}

            for (int i = 0; i < 24; ++i)
            {
                GameObject c = Object.Instantiate(InventoryItemUIPrefab[(int)ItemTag.Medkit], this.gameObject.transform);
                var handler = c.GetComponent<InventoryItemHandler>();
                if (handler != null)
                {
                    handler.Configure(ItemTag.Medkit, i);
                }
            }
            m_InventorySelector = PlayerInventory.TryConsumeInventorySelection();
        }

        RefreshVisualSelector();
        RefreshCanUseItem(null);
        RefreshCanTrashItem(null);
    }

    public void Selected(InventoryItemHandler handler)
    {
        m_CurrentItem = handler;
        RefreshVisualSelector();
        RefreshOptions();
    }

    void RefreshVisualSelector()
    {
        if (VisualIndicator)
        {
            if (m_CurrentItem)
            {
                VisualIndicator.transform.position = m_CurrentItem.gameObject.transform.position;
            }
            VisualIndicator.SetActive(m_CurrentItem != null && m_CurrentItem.IsViableItem());
        }
    }
    void RefreshOptions()
    {
        if (m_InventorySelector)
        {
            m_InventorySelector.SetItemToShow(m_CurrentItem ? m_CurrentItem.m_Tag : ItemTag.COUNT);
            CloseInventoryScene();
        }
        else
        {
            RefreshCanUseItem(m_CurrentItem);
            RefreshCanTrashItem(m_CurrentItem);
        }
    }

    private void RefreshCanTrashItem(InventoryItemHandler currentItem)
    {
        if (TrashItem)
        {
            bool canTrash = currentItem ? PlayerInventory.CanDelete(currentItem.m_Tag) : false;
            TrashItem.SetActive(canTrash);
        }
    }

    private void RefreshCanUseItem(InventoryItemHandler currentItem)
    {
        if (ConsumeItem)
        {
            bool canInvoke = currentItem ? PlayerInventory.HasAction(currentItem.m_Tag) : false;
            ConsumeItem.SetActive(canInvoke);
        }
    }

    public void Click_ConsumeItem()
    {
        PlayerInventory.UseItem(m_CurrentItem.m_Tag);
        m_CurrentItem.Hide();
        RefreshVisualSelector();
    }

    public void Click_TrashItem()
    {
        PlayerInventory.RemoveItem(m_CurrentItem.m_Tag);
        m_CurrentItem.Hide();
        RefreshVisualSelector();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Closing && OpenInventoryAction.ReadValue<float>() > 0)
        {
            CloseInventoryScene();
        }
    }

    public void CloseInventory_Click()
    {
        CloseInventoryScene();
    }


    void LoadItemInInventoryUI()
    {

    }

    void ReportLastSelectInventoryItem()
    {

    }

    void CloseInventoryScene()
    {
        OpenInventoryAction.Disable();
        SceneManager.UnloadSceneAsync("InventoryScene");
    }
}
