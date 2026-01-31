using UnityEngine;

public class ItemDropSpawner : MonoBehaviour
{
    GameObject[] pickupSpawns;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropRandomItemAtSpot(Transform transform)
    {
        int randomSpawn = Random.Range(0, pickupSpawns.Length);
        GameObject.Instantiate(pickupSpawns[randomSpawn], transform);
    }
}
