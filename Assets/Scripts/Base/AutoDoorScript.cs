using Unity.VisualScripting;
using UnityEngine;

public class AutoDoorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject exitSignObject;
    [SerializeField]
    private GameObject doorObject;
    [SerializeField]
    private Vector3 closedPosition;
    [SerializeField]
    private Vector3 openPosition;

    [SerializeField]
    private float timeToOpen;

    private bool isOpening;
    private float elapsedOpenTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpening = false;
        elapsedOpenTime = 0.0f;
        doorObject.transform.localPosition = closedPosition;

        if (exitSignObject)
        {
            exitSignObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            if (elapsedOpenTime < timeToOpen)
            {
                elapsedOpenTime += Time.deltaTime;
            }
        }
        else
        {
            if (elapsedOpenTime > 0.0f)
            {
                elapsedOpenTime -= Time.deltaTime;
            }
        }

        doorObject.transform.localPosition = Vector3.Lerp(closedPosition, openPosition, elapsedOpenTime / timeToOpen);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == null)
            return;

        if (collider.gameObject.tag == "Player")
        {
            isOpening = true;
            
            if(exitSignObject)
            {
                exitSignObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == null)
            return;

        if (collider.gameObject.tag == "Player")
        {
            isOpening = false;

            if (exitSignObject)
            {
                exitSignObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(closedPosition, 1.0f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(openPosition, 1.0f);
    }
}
