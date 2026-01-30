using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float maxViewDistance;

    Collider2D myCollider;

    [SerializeField]
    float movementSpeed;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myCollider = this.GetComponent<Collider2D>();
        rb = this.GetComponent<Rigidbody2D>();
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
        List<RaycastHit2D> hitObjects = Physics2D.RaycastAll(this.transform.position, directionToPlayer, maxViewDistance).Where(x => x.collider != myCollider).ToList();
        hitObjects.AddRange(Physics2D.RaycastAll(this.transform.position, Quaternion.Euler(0, 0, 15) * directionToPlayer, maxViewDistance).Where(x => x.collider != myCollider).ToList());
        hitObjects.AddRange(Physics2D.RaycastAll(this.transform.position, Quaternion.Euler(0, 0, -15) * directionToPlayer, maxViewDistance).Where(x => x.collider != myCollider).ToList());
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
        rb.AddForce(dirToPlayer * movementSpeed * Time.deltaTime);
    }
}
