using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float sprintModifier;

    [SerializeField]
    float sneakModifier;

    [SerializeField]
    Vector3 movementDirection;

    [SerializeField]
    float oxygenUseScalar;

    [SerializeField]
    PlayerAnimationManager playerAnimationManager;

    // Movement inputs
    InputAction moveAction;
    InputAction sprintAction;
    InputAction sneakAction;
    InputAction interactionAction;

    PlayerStats stats;
    Rigidbody rb;

    Vector3 lastFramePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        sneakAction = InputSystem.actions.FindAction("Crouch");
        interactionAction = InputSystem.actions.FindAction("Interact");
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }

    Vector3 GetDiagonalOnlyMovementDirection(Vector2 movementInput)
    {
        Vector3 direction = Vector3.zero;

        if (movementInput.y > 0.0f)
        {
            direction = new Vector3(-1, 0, 1);
        }
        
        if (movementInput.y < 0.0f)
        {
            direction = new Vector3(1, 0, -1);
        }

        if (movementInput.x > 0.0f)
        {
            direction = new Vector3(1, 0, 1);
        }

        if (movementInput.x < 0.0f)
        {
            direction = new Vector3(-1, 0, -1);
        }

        return direction;
    }

    // Update is called once per frame
    void Update()
    {
        float thisFrameOxygenMod = 0;

        lastFramePos = transform.position;

        float thisFrameMoveSpeed = movementSpeed;
        movementDirection = GetDiagonalOnlyMovementDirection(moveAction.ReadValue<Vector2>());

        if (movementDirection.magnitude > 0)
        {
            thisFrameOxygenMod += 5;
        }

        if (sprintAction.ReadValue<float>() > 0)
        {
            thisFrameMoveSpeed += sprintModifier;
            thisFrameOxygenMod += 10;
        }

        if (sneakAction.ReadValue<float>() > 0)
        {
            thisFrameMoveSpeed += sneakModifier;
            thisFrameOxygenMod -= 3;
        }

        if (playerAnimationManager.CanMove())
        {
            stats.OxygenUsagePerSecond = thisFrameOxygenMod * oxygenUseScalar;

            stats.OxygenUsagePerSecond = thisFrameOxygenMod * oxygenUseScalar;

            Vector3 movement = movementDirection * thisFrameMoveSpeed * Time.deltaTime;
            rb.AddForce(movement);
            playerAnimationManager.SetLastFrameMovement(movement);

            if (interactionAction.triggered)
            {
                Debug.DrawLine(transform.position, transform.position + (transform.forward * 100.0f), Color.red, 5.0f);
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
                {
                    BaseMachine machineInteracted = hit.collider.gameObject.GetComponent<BaseMachine>();
                    if (machineInteracted)
                    {                   
						machineInteracted.UseMachine(null);
                    }
                }
            }
        }
    }
}