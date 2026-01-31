using UnityEngine;

public class WeaponController : MonoBehaviour 
{

    public Transform weapon;
    public float maxDistance = 20f;
    public LayerMask hitMask;

    void Update()
    {
        AimAtCursor();

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void AimAtCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - weapon.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - weapon.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(weapon.position, direction, maxDistance, hitMask);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
}

    
    


