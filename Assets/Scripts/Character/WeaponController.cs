using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public Transform weapon;
    public float maxDistance = 20f;
    public LayerMask hitMask;
    
    Collider myCollider;

    private InputAction shootAction;

    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
        myCollider = weapon.GetComponent<Collider>();
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
        Plane aimPlane  = new Plane(Vector3.up, Vector3.zero);
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        float entryPoint;
        Vector3 aimPoint;

        if (aimPlane.Raycast(ray, out entryPoint))
        {
            aimPoint = ray.GetPoint(entryPoint);
        }
        else
        {
            aimPoint = transform.position + transform.forward * 10f;
        }
        
        Vector3 origin = weapon.transform.position;
        Vector3 direction = (aimPoint - origin).normalized;
        
        
        
        Debug.DrawRay(origin, direction * maxDistance, Color.green);
    }
}