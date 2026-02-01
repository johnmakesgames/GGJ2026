using UnityEngine;

public class OxygenMachine : BaseMachine
{
    [SerializeField]
    float oxygenIncreasePerSecond;

    [SerializeField]
    Sprite inactiveSprite;
    [SerializeField]
    Sprite activeSprite;
    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool UseMachine(object? contextObj)
    {
        if (GetNearbyPlayerGameObject() == null)
            return false;

        PlayerStats stats = GetNearbyPlayerGameObject().GetComponent<PlayerStats>();

        if(stats)
        {
            stats.CurrentOxygen += oxygenIncreasePerSecond * Time.deltaTime;
        }

        Debug.Log("OXYGEN OXYGEN OXYGEN BRR BRR BRR");

        return true;
    }
}
