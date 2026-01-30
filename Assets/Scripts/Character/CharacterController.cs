using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float sprintModifier;

    [SerializeField]
    float sneakModifier;

    [SerializeField]
    Vector3 movementDirection;

    // Movement inputs
    InputAction moveAction;
    InputAction sprintAction;
    InputAction sneakAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        sneakAction = InputSystem.actions.FindAction("Crouch");
    }

    // Update is called once per frame
    void Update()
    {
        float thisFrameMoveSpeed = movementSpeed;
        movementDirection = moveAction.ReadValue<Vector2>();

        if (sprintAction.ReadValue<float>() > 0)
        {
            thisFrameMoveSpeed += sprintModifier;
        }
        
        if (sneakAction.ReadValue<float>() > 0)
        {
            thisFrameMoveSpeed += sneakModifier;
        }

        this.transform.position += (movementDirection * thisFrameMoveSpeed * Time.deltaTime);
    }
}
