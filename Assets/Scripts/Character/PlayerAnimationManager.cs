using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    InputAction attackAction;
    float timeSinceShot = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    public void SetIsPlayer(bool isPlayer)
    {
        animator.SetBool("IsPlayer", isPlayer);
    }

    public void SetLastFrameMovement(Vector3 movement)
    {
        animator.SetFloat("MoveSpeed", movement.magnitude);
        animator.SetFloat("MoveDirection", movement.z);
        animator.SetFloat("TimeSinceShot", timeSinceShot);

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (attackAction.ReadValue<float>() > 0.0f & timeSinceShot >= 0.5f)
        {
            animator.SetTrigger("Shoot");
            timeSinceShot = 0;
        }

        timeSinceShot += Time.deltaTime;
    }

    public bool CanMove()
    {
        return !animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.ToLower().Contains("shoot");
    }
}