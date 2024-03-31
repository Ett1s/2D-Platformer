using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ControllerHeropicsel : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Collision2D collision;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 7f;
    private bool isGrounded = false;
    private bool isTakingDamage = false;

    private GameObject enemy;

    // Health variables
    [SerializeField] public int maxHealth = 5;
    private int currentHealth;

    // Attack delay variables
    private bool isAttacking = false;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float damageDelay = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        
        currentHealth = maxHealth; // Initialize health
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

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("attack", true);

            // Check for collision with enemy during attack
            //if (collision != null && collision.gameObject.CompareTag("Enemy"))
            //{
            //    SkeletonBehavior enemyScript = collision.gameObject.GetComponent<SkeletonBehavior>();
            //    enemyScript.TakeDamage(1); // Call TakeDamage on the enemy
            //}

            StartCoroutine(AttackDelay());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isTakingDamage)
            {
                StartCoroutine(TakeDamageOverTime(1, 0.5f)); // Ќаносить 1 единицу урона каждую секунду
            }

            //if (Input.GetMouseButtonDown(0) && !isAttacking)
            //{
            //    isAttacking = true;
            //    animator.SetBool("attack", true);
            //    SkeletonBehavior enemyscript = enemy.GetComponent<SkeletonBehavior>();
            //    enemyscript.TakeDamage(1);
            //    StartCoroutine(AttackDelay());
            //}
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StopCoroutine(TakeDamageOverTime(1, 1f)); // ќстановить нанесение урона, когда герой уходит от противника
            isTakingDamage = false;
        }
    }

    IEnumerator TakeDamageOverTime(int damage, float delay)
    {
        isTakingDamage = true;
        while (isTakingDamage)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(delay);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Play death animation
            animator.SetTrigger("Death");

            // Disable movement
            GetComponent<Rigidbody2D>().isKinematic = true;

            // Destroy player after a delay (optional)
            StartCoroutine(DestroyPlayer(2f));
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        animator.SetBool("attack", false);
    }

    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(damageDelay);
    }

    IEnumerator DestroyPlayer(float delay) // Uncomment if you want to destroy the player
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}