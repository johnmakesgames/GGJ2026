using UnityEngine;
using UnityEngine.InputSystem;

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
        animator.SetBool("IsPlayer", isPlayer);
    }

    private void Update()
    {
        timeSinceShot += Time.deltaTime;
    }

    public void SetLastFrameMovement(Vector3 movement)
    {
        animator.SetFloat("MoveSpeed", movement.magnitude);
        animator.SetFloat("MoveDirection", movement.z);
        animator.SetBool("JustShot", false);

        if (CanMove())
        {
            if (movement.x > 0)
            {
                spriteRenderer.flipX = false;
                Debug.Log("Flipped by movement Right");
            }
            else if (movement.x < 0)
            {
                spriteRenderer.flipX = true;
                Debug.Log("Flipped by movement Left");
            }
        }

        if (attackAction.ReadValue<float>() > 0.0f & timeSinceShot >= 0.25f && isPlayer)
        {
            Vector2 mouseLocation = Mouse.current.position.ReadValue();
            mouseLocation.x /= Screen.width;
            mouseLocation.y /= Screen.height;

            mouseLocation.x -= 0.5f;
            mouseLocation.y -= 0.5f;

            if (mouseLocation.x > 0)
            {
                spriteRenderer.flipX = false;
                Debug.Log("Flipped by mouse Right");
            }
            else if (mouseLocation.x < 0)
            {
                spriteRenderer.flipX = true;
                Debug.Log("Flipped by mouse Left");
            }

            animator.SetFloat("MoveDirection", mouseLocation.y);
            animator.SetBool("JustShot", true);
            animator.SetTrigger("Shoot");
            timeSinceShot = 0;
        }
    }

    public bool CanMove()
    {
        bool shooting = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.ToLower().Contains("shoot");

        if (shooting)
        {
            Debug.Log("Can't move due to shoot");
        }

        return !shooting && (timeSinceShot > 0.5f);
    }
}