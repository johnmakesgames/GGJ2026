using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{
    public Transform weapon;
    public float maxDistance = 40f;
    public LayerMask hitMask;

    [SerializeField] private float shotgonFireRate = 0.4f;
    [SerializeField] private float shotgonReloadTime = 1f;
    private float delay;

    [SerializeField] public int ammoStockpile = 10;

    [SerializeField] private int ammo = 2;

    [SerializeField] Collider player;

    private InputAction shootAction;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunshotClip;

    public int GetAmmoInGun()
    {
        return ammo;
    }

    public void AddAmmoToStockpile(int ammo)
    {
        ammoStockpile += ammo;
    }

    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
        audioSource =  GetComponent<AudioSource>();
    }

    void Update()
    {
        if (ammoStockpile > 0 && ammo == 0)
        {
            Reload();
            return;
        }
        
        if (shootAction.WasPressedThisFrame() && (SceneManager.GetActiveScene().name != "BaseScene"))
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (ammo == 0 && ammoStockpile == 0)
            return;

        if (Time.time < delay)
            return;
        
        delay = Time.time + shotgonFireRate;

        Shoot();
    }

    void Reload()
    {
        delay = Time.time + shotgonReloadTime;
        Debug.Log("Reloading" + delay);

        ammo += 2;
        ammoStockpile -= 2;

        Debug.Log("Ammo: " + ammo + " Stock: " + ammoStockpile);
    }


    void Shoot()
    {
        ammo--;
        
        if(gunshotClip != null)
            audioSource.PlayOneShot(gunshotClip);

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
                enemyHealth.CurrentHealth--;
                Debug.Log("Enemy health: " + enemyHealth.CurrentHealth);
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
            return null;
        }

        return hits[0].transform.root.gameObject.GetComponent<Health>();
    }
}