using UnityEngine;

public class ItemDropSpawner : MonoBehaviour
{
    [SerializeField]
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
        GameObject spawnedPickup = Instantiate(pickupSpawns[randomSpawn], new Vector3(0, 0, 0), Quaternion.identity);
        spawnedPickup.transform.position = transform.position;
    }
}
