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
        if (OpenInventoryAction.ReadValue<float>() > 0)
        {
            OpenInventoryScene();
        }
    }

    void OpenInventoryScene()
    {
        //check scene is not open
        bool isLoaded = SceneManager.GetSceneByName("InventoryScene").isLoaded;
        if (!isLoaded)
        {
            SceneManager.LoadScene("InventoryScene", LoadSceneMode.Additive);
        }
    }
}
