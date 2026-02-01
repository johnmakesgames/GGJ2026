using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OpenInventory : MonoBehaviour
{
    InputAction OpenInventoryAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenInventoryAction = InputSystem.actions.FindAction("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenInventoryAction != null)
        {
            if (OpenInventoryAction.IsPressed())
            {
                OpenInventoryScene();
            }
            else
            {
                if (!OpenInventoryAction.enabled)
                {
                    if (!SceneManager.GetSceneByName("InventoryScene").isLoaded)
                    {
                        OpenInventoryAction.Enable();
                    }
                }
            }
        }
    }

    public void OpenInventory_Click()
    {
        OpenInventoryScene();
    }
    void OpenInventoryScene()
    {
        //check scene is not open
        bool isLoaded = SceneManager.GetSceneByName("InventoryScene").isLoaded;
        if (!isLoaded)
        {
            OpenInventoryAction.Disable();
            SceneManager.LoadSceneAsync("InventoryScene", LoadSceneMode.Additive);
        }
    }
}
