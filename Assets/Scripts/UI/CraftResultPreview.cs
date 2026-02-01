using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CraftResultPreview : MonoBehaviour
{
    [SerializeField]
    InventorySelector item1;

    [SerializeField]
    InventorySelector item2;

    [SerializeField]
    CraftingMachine craftingMachine;

    Image image;

    [SerializeField]
    Sprite[] orderedItemSprites;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ItemTag result = craftingMachine.GetRecipeResult(item1.GetCurrentSelection(), item2.GetCurrentSelection());
        image.sprite = orderedItemSprites[(int)result];
    }
}
