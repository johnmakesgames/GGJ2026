using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    [SerializeField]
    SpriteRenderer spriteRenderer;

    public bool isCured;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myCollider = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();

        this.GetComponent<Health>().OnDamage += OnDamage;
        this.GetComponent<Health>().OnDeath += OnDeath;

        isCured = false;
    }

    float counter = 0;
    // Update is called once per frame
    void Update()
    {
        if (!isCured)
        {
            if (TestVisionToPlayer())
            {
                MoveTowardsPlayer();
            }

            counter += Time.deltaTime;
            if (counter > 5.0f)
            {
                Cure();
            }
        }
        else
        {
            MoveTowardsSaferoom();
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

    void MoveTowardsSaferoom()
    {
        Vector3 dirToSaferoom = Vector3.Normalize(new Vector3(0,this.transform.position.y,0) - this.transform.position);

        Vector3 totalFrameMovement = dirToSaferoom * (movementSpeed * 1.5f) * Time.deltaTime;
        rb.AddForce(totalFrameMovement);
        playerAnimationManager.SetLastFrameMovement(totalFrameMovement);

        Color newColor = spriteRenderer.color;
        newColor.a -= 0.1f * Time.deltaTime;
        spriteRenderer.color = newColor;
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

    private IEnumerator coroutine;
    public void Cure()
    {
        isCured = true;
        spriteRenderer.color = Color.white;
        coroutine = KilLSelfOnceDurationPassed(10.0f);
        player.GetComponent<PlayerStats>().SignalCured();
        StartCoroutine(coroutine);
    }

    private IEnumerator KilLSelfOnceDurationPassed(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            OnDeath();
        }
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
