using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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
    PlayerAnimationManager playerAnimationManager;

    // Movement inputs
    InputAction moveAction;
    InputAction sprintAction;
    InputAction sneakAction;

    Rigidbody rb;

    Vector3 lastFramePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        sneakAction = InputSystem.actions.FindAction("Crouch");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lastFramePos = transform.position;

        float thisFrameMoveSpeed = movementSpeed;
        movementDirection = moveAction.ReadValue<Vector2>();
        movementDirection.z = movementDirection.y;
        movementDirection.y = 0;

        if (sprintAction.ReadValue<float>() > 0)
        {
            thisFrameMoveSpeed += sprintModifier;
        }
        
        if (sneakAction.ReadValue<float>() > 0)
        {
            thisFrameMoveSpeed += sneakModifier;
        }

        thisFrameMoveSpeed *= Time.deltaTime;

        Vector3 movement = movementDirection * thisFrameMoveSpeed;
        rb.AddForce(movement);
        playerAnimationManager.SetLastFrameMovement(movement);
    }
}
