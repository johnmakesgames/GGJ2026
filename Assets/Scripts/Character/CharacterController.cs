using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    Vector3 movementDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

        }

        if (Input.GetKeyDown(KeyCode.S))
        {

        }

        if (Input.GetKeyDown(KeyCode.A))
        {

        }

        if (Input.GetKeyDown(KeyCode.D))
        {

        }
    }
}
