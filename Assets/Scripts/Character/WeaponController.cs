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
        var scrPoint = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Ray ray = Camera.main.ScreenPointToRay(scrPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            Debug.DrawRay(Camera.main.transform.position, (hitPoint - Camera.main.transform.position), Color.red);

            Vector3 origin = weapon.transform.position;
            Vector3 direction = (hitPoint - origin).normalized;
            Debug.DrawRay(origin, direction * maxDistance, Color.green);

            if (Physics.Raycast(origin, direction, out hit, maxDistance, hitMask))
            {
                var enemy =  hit.collider.GetComponent<EnemyController2D>();
                
            }
            
            
        }
    }

    
}