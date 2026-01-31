using UnityEngine;

public class OxygenMachine : BaseMachine
{
    [SerializeField]
    float oxygenIncreasePerSecond;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UseMachine()
    {
        if (GetNearbyPlayerGameObject() == null)
            return;

        PlayerStats stats = GetNearbyPlayerGameObject().GetComponent<PlayerStats>();

        if(stats)
        {
            stats.CurrentOxygen += oxygenIncreasePerSecond * Time.deltaTime;
        }

        Debug.Log("OXYGEN OXYGEN OXYGEN BRR BRR BRR");
    }
}
