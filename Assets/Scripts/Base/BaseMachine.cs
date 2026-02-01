using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BaseMachine : MonoBehaviour
{
    [SerializeField]
    GameObject machineUIRoot;

    private bool isPlayerNearby;
    GameObject nearbyPlayerGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void Awake()
    {
        isPlayerNearby = false;
        nearbyPlayerGameObject = null;
        machineUIRoot.SetActive(isPlayerNearby);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayerNearby()
    {
        return isPlayerNearby;
    }

    public GameObject GetNearbyPlayerGameObject()
    {
        return nearbyPlayerGameObject;
    }

    public virtual bool UseMachine(object? contextObj)
    {
        return false;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null)
            return;

        if (other.gameObject.tag == "Player")
        {
            isPlayerNearby = true;
            nearbyPlayerGameObject = other.gameObject;

            if (machineUIRoot)
            {
                machineUIRoot.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == null)
            return;

        if (other.gameObject.tag == "Player")
        {
            isPlayerNearby = false;
            nearbyPlayerGameObject = null;

            if (machineUIRoot)
            {
                machineUIRoot.SetActive(false);
            }
        }
    }

}
