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
    private int playerHitCount = 0; // Track player hit count
    private float deathTimer = 0f; // Timer for death animation
    private bool isPlayerDead = false; // Flag to track player death

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        animator.SetBool("attack", false);
        animator.SetBool("walk", false);

        // Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (!isPlayerDead && distanceToPlayer <= attackRange) // Attack only if player is alive
        {
            // Attack player
            animator.SetBool("attack", true);
        }

        if (!isPlayerDead && distanceToPlayer <= 5f) // Move only if player is alive
        {
            animator.SetBool("walk", true);
            // Move towards player
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
            // Flip sprite based on direction
            sprite.flipX = direction.x < 0.0f;
        }

        // Handle death animation and disappearance
        if (deathTimer > 0f)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player hit
            playerHitCount++;

            if (playerHitCount >= 5)
            {
                // Kill player on 5th hit
                Animator playerAnimator = collision.gameObject.GetComponent<Animator>();
                playerAnimator.SetTrigger("Death");

                // Disable player movement immediately
                GetComponent<Rigidbody2D>().isKinematic = true;

                // Set player dead flag
                isPlayerDead = true;

                // Destroy player after 2 seconds (optional)
                Destroy(collision.gameObject, 2f);  // You can uncomment this if you want the player to disappear as well
            }
        }
    }
}