using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ControllerHeropicsel : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 7f;
    private bool isGrounded = false;
    private int enemyHitCount = 0; // Track enemy hit count

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetBool("attack", false);
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            Vector3 dir = transform.right * Input.GetAxis("Horizontal");
            float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
            sprite.flipX = dir.x < 0.0f;

            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("attack", true);
            enemyHitCount++; // Increment hit count on attack
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            enemyHitCount = 0; // Reset hit count on ground touch (optional)
        }
        else if (collision.gameObject.CompareTag("Enemy") && enemyHitCount >= 2)
        {
            // Trigger enemy death animation
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
            if (enemyAnimator != null) // Check if enemy has animator
            {
                enemyAnimator.SetTrigger("Death");

                // Disable enemy movement (if needed)
                collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                // Destroy enemy after 2 seconds
                StartCoroutine(DestroyEnemy(collision.gameObject, 2f));
            }
            else
            {
                Debug.LogWarning("Enemy missing Animator component. Death animation cannot be triggered.");
            }
            enemyHitCount = 0; // Reset hit count after kill
        }
    }

    IEnumerator DestroyEnemy(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(enemy);
    }
}