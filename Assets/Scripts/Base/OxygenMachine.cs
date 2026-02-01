using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class OxygenMachine : BaseMachine
{
    [SerializeField]
    float oxygenIncreasePerSecond;
    [SerializeField]
    float increasePerUpgrade;
    [SerializeField]
    bool fillTankOnUpgrade;

    [Header("Sprite Stuff")]
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite inactiveSprite;
    [SerializeField]
    Sprite activeSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spriteRenderer)
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public void OnTriggerStay(Collider other)
    {
        if (GetNearbyPlayerGameObject() != null && other.gameObject.tag == "Player")
        {
            PlayerStats stats = GetNearbyPlayerGameObject().GetComponent<PlayerStats>();
            spriteRenderer.sprite = activeSprite;
            stats.CurrentOxygen += oxygenIncreasePerSecond * Time.deltaTime;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        spriteRenderer.sprite = inactiveSprite;
    }

    public override bool UseMachine(object? contextObj)
    {
        if (GetNearbyPlayerGameObject() == null)
            return false;

        PlayerStats stats = GetNearbyPlayerGameObject().GetComponent<PlayerStats>();
        PlayerInventory inventory = GetNearbyPlayerGameObject().GetComponent<PlayerInventory>();

        if (stats && inventory)
        {
            if (inventory.HasItem(ItemTag.ExtraOxygenTank) &&
                inventory.HasItem(ItemTag.Slug))
            {
                ItemTag[] items = { ItemTag.ExtraOxygenTank, ItemTag.Slug };

                if (inventory.RemoveItems(items))
                {
                    stats.IncreaseMaxOxygen(increasePerUpgrade, fillTankOnUpgrade);
                }
            }
        }

        Debug.Log("OXYGEN OXYGEN OXYGEN BRR BRR BRR");

        return true;
    }


}
