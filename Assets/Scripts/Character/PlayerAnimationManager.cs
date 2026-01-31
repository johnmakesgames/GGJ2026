using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetLastFrameMovement(Vector3 movement)
    {
        animator.SetFloat("MoveSpeed", movement.magnitude);

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
