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


        if (distanceToPlayer <= attackRange)
        {
            // Attack player
            animator.SetBool("attack", true);
        }

        if (distanceToPlayer <= 5f) // Start moving towards player if within 5 units
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
}
