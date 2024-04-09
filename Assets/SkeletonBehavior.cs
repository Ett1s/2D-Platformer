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
    [SerializeField] private float attackRadius = 200f;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] public AudioClip deathSound;
    [SerializeField] private AudioClip walkStepSound;
    private Coroutine attackSoundCoroutine;
    private GameObject player;
    private AudioSource audioSource;

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
        if (currentHealth <= 0) return; // Don't do anything if dead

        animator.SetBool("attack", false);
        animator.SetBool("walk", false);

        // Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            Debug.Log("called3");
            // Attack player
            animator.SetBool("attack", true);
            if (attackSoundCoroutine == null)
            {
                attackSoundCoroutine = StartCoroutine(PlayAttackSound());
            }

            // Check for collision with player during attack
            Collider2D[] playersHit = Physics2D.OverlapCircleAll(transform.position, attackRadius);
            foreach (Collider2D playerCollider in playersHit)
            {
                Debug.Log("called2");
                //if (playerCollider.CompareTag("Player"))
                //{
                    Debug.Log("called1");
                    ControllerHeropicsel playerScript = playerCollider.GetComponent<ControllerHeropicsel>();
                    if (playerScript != null)
                    {
                        Debug.Log("called");
                        playerScript.TakeDamage(1); // Deal damage to the player
                    }
                //}
            }
        }
        else
        {
            // End of attack
            animator.SetBool("attack", false);
            if (attackSoundCoroutine != null)
            {
                StopCoroutine(attackSoundCoroutine);
                attackSoundCoroutine = null;
            }
        }

        if (distanceToPlayer <= 5f)
        {
            animator.SetBool("walk", true);
            // Move towards player
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
            // Flip sprite based on direction
            sprite.flipX = direction.x > 0.0f;
        }
    }


    private void ApplyDamageToPlayer()
    {
        ControllerHeropicsel playerScript = player.GetComponent<ControllerHeropicsel>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(1); // Deal damage to the player
            Debug.Log("Skeleton dealt damage to player!"); // For debugging
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

            // Play death sound
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position); // Play the sound at the character's position
            }

            // Disable movement and collision
            rb.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;

            // Destroy after a delay (optional)
            StartCoroutine(DestroyEnemy(2f));
        }
    }
    public void PlayWalkStepSound()
    {
        if (walkStepSound != null)
        {
            AudioSource.PlayClipAtPoint(walkStepSound, transform.position);
        }
    }

    IEnumerator DestroyEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    IEnumerator PlayAttackSound()
    {
        while (animator.GetBool("attack"))
        {
            if (attackSound != null)
            {
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
            }
            yield return new WaitForSeconds(1f); // Задержка в 1 секунду
        }
    }
}