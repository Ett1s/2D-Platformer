using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackRange = 10f;
    private GameObject player;

    [SerializeField] public int maxHealth = 2;
    private int currentHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth; // Initialize health
    }

    void Update()
    {
        animator.SetBool("attack", false);
        animator.SetBool("walk", false);

        // Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange) // Attack only if player is alive
        {
            // Attack player
            animator.SetBool("attack", true);
        }

        if (distanceToPlayer <= 5f) // Move only if player is alive
        {
            animator.SetBool("walk", true);
            // Move towards player
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
            // Flip sprite based on direction
            sprite.flipX = direction.x < 0.0f;
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Skeleton Damage received. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            // Play death animation
            animator.SetTrigger("Death");

            // Disable movement
            GetComponent<Rigidbody2D>().isKinematic = true;

            // Destroy player after a delay (optional)
            StartCoroutine(DestroyEnemy(2f));
        }
    }

    IEnumerator DestroyEnemy(float delay) // Uncomment if you want to destroy the player
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}