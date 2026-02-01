using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public Transform weapon;
    public float maxDistance = 20f;
    public LayerMask hitMask;

    [SerializeField] private float pistolFireRate = 0.002f;
    float delay = 0.0f;

    [SerializeField] public int Ammo;


    [SerializeField] Collider player;

    private InputAction shootAction;


    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        if (shootAction.ReadValue<float>() > 0)
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (Time.time < delay)
            return;

        delay += Time.deltaTime + pistolFireRate;
        Shoot();
    }


    void Shoot()
    {
        Ammo--;
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

            Health enemyHealth = EnemyHit(direction);

            if (enemyHealth != null)
            {
                Debug.Log("Hit");
                enemyHealth.CurrentHealth--;
                Debug.Log("Enemy health: " + enemyHealth.CurrentHealth);
            }
            else
            {
                Debug.Log("Miss");
            }
        }
    }

    Health EnemyHit(Vector3 hitPoint)
    {
        List<RaycastHit> hits = Physics.RaycastAll(this.transform.position, hitPoint, maxDistance)
            .Where(x => x.collider != player)
            .OrderBy(y => y.distance).ToList();

        if (hits.Count() <= 0)
        {
            Debug.Log("No Hit");
        }

        return hits[0].transform.root.gameObject.GetComponent<Health>();
    }
}