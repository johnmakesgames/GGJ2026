using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float maxViewDistance;

    Collider myCollider;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float touchDamage;

    Rigidbody rb;

    [SerializeField]
    PlayerAnimationManager playerAnimationManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myCollider = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();

        this.GetComponent<Health>().OnDamage += OnDamage;
        this.GetComponent<Health>().OnDeath += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (TestVisionToPlayer())
        {
            MoveTowardsPlayer();
        }
    }

    bool TestVisionToPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - this.transform.position;
        List<RaycastHit> hitObjects = Physics.RaycastAll(this.transform.position, directionToPlayer, maxViewDistance).Where(x => x.collider != myCollider).ToList();
        hitObjects.AddRange(Physics.RaycastAll(this.transform.position, Quaternion.Euler(0, 15, 0) * directionToPlayer, maxViewDistance).Where(x => x.collider != myCollider).ToList());
        hitObjects.AddRange(Physics.RaycastAll(this.transform.position, Quaternion.Euler(0, -15, 0) * directionToPlayer, maxViewDistance).Where(x => x.collider != myCollider).ToList());
        hitObjects.OrderBy(x => x.distance).ToList();

        if (hitObjects.Count > 0)
        {
            if (hitObjects[0].transform.gameObject == player)
            {
                return true;
            }
        }

        return false;
    }

    void MoveTowardsPlayer()
    {
        Vector3 dirToPlayer = Vector3.Normalize(player.transform.position - this.transform.position);

        Vector3 totalFrameMovement = dirToPlayer * movementSpeed * Time.deltaTime;
        rb.AddForce(totalFrameMovement);
        playerAnimationManager.SetLastFrameMovement(totalFrameMovement);
    }

    void OnDamage()
    {
        // Do something maybe sound or flash or something
    }

    void OnDeath()
    {
        this.GetComponent<Health>().OnDamage -= OnDamage;
        this.GetComponent<Health>().OnDeath -= OnDeath;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        if (collision.gameObject == player)
        {
            Debug.Log("It is player");

            PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                Debug.Log("Player has health");
                playerHealth.CurrentHealth -= touchDamage;
            }
        }
    }
}
