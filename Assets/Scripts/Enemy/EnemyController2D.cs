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

    private WorldUIController worldUIController;

    ~EnemyController2D()
    {
        this.GetComponent<Health>().OnDamage -= OnDamage;
        this.GetComponent<Health>().OnDeath -= OnDeath;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myCollider = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();

        this.GetComponent<Health>().OnDamage += OnDamage;
        this.GetComponent<Health>().OnDeath += OnDeath;

        worldUIController = GameObject.FindGameObjectWithTag("WorldUI").GetComponent<WorldUIController>();

        isCured = false;

        playerAnimationManager.SetIsPlayer(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCured)
        {
            if (TestVisionToPlayer())
            {
                MoveTowardsPlayer();
            }
            else
            {
                playerAnimationManager.SetLastFrameMovement(Vector3.zero);
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
        Vector3 safeRoom = new Vector3(-11.27f, 3.08f, 39.9f);
        Vector3 dirToSaferoom = Vector3.Normalize(safeRoom - this.transform.position);

        Vector3 totalFrameMovement = dirToSaferoom * (movementSpeed * 1.5f) * Time.deltaTime;
        rb.AddForce(totalFrameMovement);
        playerAnimationManager.SetLastFrameMovement(totalFrameMovement);

        Color newColor = spriteRenderer.color;
        newColor.a -= 0.1f * Time.deltaTime;
        spriteRenderer.color = newColor;
    }

    void OnDamage(float dmg)
    {
        worldUIController.ShowDamage(dmg, gameObject.transform.position, this.gameObject, false);
    }

    void OnDeath()
    {
        if (!isCured)
        {
            GameObject.FindGameObjectWithTag("ItemDropper").GetComponent<ItemDropSpawner>().DropRandomItemAtSpot(this.transform);
        }

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

    float timeSinceLastHitTick = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                playerHealth.CurrentHealth -= touchDamage;
                timeSinceLastHitTick = 0;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        timeSinceLastHitTick += Time.deltaTime;
        if (collision.gameObject == player && timeSinceLastHitTick >= 0.5f)
        {
            PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                playerHealth.CurrentHealth -= touchDamage;
                timeSinceLastHitTick = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player && timeSinceLastHitTick >= 0.5f)
        {
            PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                timeSinceLastHitTick = 0;
            }
        }
    }
}