using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    InputAction attackAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    public void SetLastFrameMovement(Vector3 movement)
    {
        animator.SetFloat("MoveSpeed", movement.magnitude);
        animator.SetFloat("MoveDirection", movement.z);

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (attackAction.ReadValue<float>() > 0.0f)
        {
            animator.SetTrigger("Shoot");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanMove()
    {
        return !animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.ToLower().Contains("shoot");
    }
}
