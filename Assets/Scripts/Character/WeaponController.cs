using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public Transform weapon;
    public float maxDistance = 20f;
    public LayerMask hitMask;

    private InputAction shootAction;

    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        if (shootAction.ReadValue<float>() > 0)
        {
            Shoot();
        }
    }
    

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        Vector3 rayDirection = (mousePos - weapon.position).normalized;

        Debug.DrawRay(weapon.position, rayDirection, Color.green);
    }
}