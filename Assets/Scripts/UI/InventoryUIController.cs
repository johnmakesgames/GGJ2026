using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InventoryUIController : MonoBehaviour
{
    InputAction OpenInventoryAction;

    public GameObject[] InventoryItemUIPrefab;

    PlayerInventory PlayerInventory;

    InventoryItemHandler m_CurrentItem = null;

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
            int i = 0;
            foreach (var item in items)
            {
                if ((int)item < InventoryItemUIPrefab.Length)
                {
                    GameObject c = Object.Instantiate(InventoryItemUIPrefab[(int)item], this.gameObject.transform);
                    var handler = c.GetComponent<InventoryItemHandler>();
                    if (handler != null)
                    {
                        handler.Configure(item, i);
                    }
                }
            }
        }
    }

    public void Selected(InventoryItemHandler handler)
    {
        m_CurrentItem = handler;
        RefreshOptions();
    }

    void RefreshOptions()
    {

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
