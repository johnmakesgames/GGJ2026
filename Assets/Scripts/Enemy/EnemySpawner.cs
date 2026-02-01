using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] PossibleEnemies;

    [SerializeField]
    float timeBetweenSpawns;
    float timeSinceSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeSinceSpawn = Random.Range(0, timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceSpawn > timeBetweenSpawns)
        {
            int randomSpawn = Random.Range(0, PossibleEnemies.Length);
            GameObject spawnedPickup = Instantiate(PossibleEnemies[randomSpawn], new Vector3(0, 0, 0), Quaternion.identity);
            spawnedPickup.transform.position = this.transform.position;
            timeSinceSpawn = 0;
        }
        else
        {
            timeSinceSpawn += Time.deltaTime;
        }
    }
}
