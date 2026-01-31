using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InventoryUIController : MonoBehaviour
{
    InputAction OpenInventoryAction;
    PlayerInventory PlayerInventory;

    bool Closing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenInventoryAction = InputSystem.actions.FindAction("Inventory");
        OpenInventoryAction.Enable();

        PlayerInventory = GameObject.FindAnyObjectByType<PlayerInventory>();
        PlayerInventory.GetAllInventoryItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Closing && OpenInventoryAction.ReadValue<float>() > 0)
        {
            CloseInventoryScene();
        }
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
