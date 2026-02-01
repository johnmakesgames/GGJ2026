using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerAnimationManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    InputAction attackAction;
    float timeSinceShot = 0;
    bool isPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    public void SetIsPlayer(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    private void Update()
    {
        timeSinceShot += Time.deltaTime;
        animator.SetBool("IsPlayer", isPlayer);
    }

    public void SetLastFrameMovement(Vector3 movement)
    {
        animator.SetFloat("MoveSpeed", movement.magnitude * 10 * Time.deltaTime);
        animator.SetFloat("MoveDirection", movement.z);
        animator.SetBool("JustShot", false);

        if (CanMove())
        {
            if (movement.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movement.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }

        
        if (attackAction.WasPerformedThisFrame() && timeSinceShot >= 0.25f && isPlayer)
        {
            if (SceneManager.GetActiveScene().name != "BaseScene")
            {
                Vector2 mouseLocation = Mouse.current.position.ReadValue();
                mouseLocation.x /= Screen.width;
                mouseLocation.y /= Screen.height;

                mouseLocation.x -= 0.5f;
                mouseLocation.y -= 0.5f;

                if (mouseLocation.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (mouseLocation.x < 0)
                {
                    spriteRenderer.flipX = true;
                }

                animator.SetFloat("MoveDirection", mouseLocation.y);
                animator.SetBool("JustShot", true);
                animator.SetTrigger("Shoot");
                timeSinceShot = 0;
            }
        }
    }

    public bool CanMove()
    {
        bool shooting = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.ToLower().Contains("shoot");
        return !shooting && (timeSinceShot > 0.5f);
    }
}