using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    private WorldUIEntityComponent[] WorldUIElementPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WorldUIElementPool = Object.FindObjectsByType<WorldUIEntityComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
