using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip Death_Sount;
    [SerializeField] private AudioClip Healh_Sound;
    public GameObject pauseMenu;
    public float pauseDelay = 2f;
    private bool isGrounded = false;
    public TextMeshProUGUI textDisplay;
    public float textSpeed;
    public string[] texts; 
    public bool HasTrigger = false;
    private bool isHealthRestored = false;


    private int currentIndex = -1;
    private Coroutine currentCoroutine;

    // Health variables
    [SerializeField] public int maxHealth = 5;
    public int currentHealth;

    public HealthBar healthBar;

    // Attack variables
    private bool isAttacking = false;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float attackRadius = 1f; // Adjust as needed

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            currentHealth = PlayerPrefs.GetInt("PlayerHealth");
            healthBar.SetHealth(currentHealth);
            Debug.Log("current health - " + currentHealth);
        }
        else
        {
            currentHealth = maxHealth; // Initialize health
            healthBar.SetMaxHealth(maxHealth); // Используйте максимальное здоровье, если нет сохраненного
            Debug.Log("max health");
        }

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Герой находится на слое Player");
        }
        else
        {
            Debug.Log("Герой не на слое Player");
        }

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

            if (attackSound != null)
            {
                AudioSource.PlayClipAtPoint(attackSound, transform.position); 
            }

            // Check for collision with enemy during attack
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRadius);
            foreach (Collider2D enemyCollider in enemiesHit)
            {
                if (enemyCollider.CompareTag("Enemy"))
                {
                    SkeletonBehavior enemyScript = enemyCollider.GetComponent<SkeletonBehavior>();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(1); 
                    }
                }
            }

            StartCoroutine(AttackDelay());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        animator.SetBool("attack", false);
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("TakeDamage called" + currentHealth);
        currentHealth -= damage;

        SaveHealth();

        healthBar.SetHealth(currentHealth);
        isHealthRestored = false;

        if (currentHealth <= 0)
        {
            Debug.Log("Skeleton Damage received. Current health: " + currentHealth);
            // Play death animation
            animator.SetTrigger("Death");

            // Disable movement
            GetComponent<Rigidbody2D>().isKinematic = true;

            AudioSource.PlayClipAtPoint(Death_Sount, transform.position);


            // Destroy player after a delay (optional)
            StartCoroutine(DestroyPlayer(2f));
            StartCoroutine(PauseAndShowMenu());

            PlayerPrefs.SetInt("PlayerHealth", maxHealth);
            PlayerPrefs.Save();
        }
    }
    public void SaveHealth()
    {
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        Debug.Log("current health - " + currentHealth);
        PlayerPrefs.Save();
    }

    public void RestoreHealth()
    {
        if (currentHealth != maxHealth && !isHealthRestored)
        {
            AudioSource.PlayClipAtPoint(Healh_Sound, transform.position);
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            SaveHealth();
            isHealthRestored = true; // Устанавливаем флаг в true после восстановления здоровья
        }
        else if (currentHealth == maxHealth)
        {
            currentIndex = (currentIndex + 1) % texts.Length; // Переход к следующему тексту
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine); // Остановить предыдущую корутину, если она есть
            currentCoroutine = StartCoroutine(ShowText(texts[currentIndex])); // Запуск новой корутины
            HasTrigger = true;
        }
    }

    IEnumerator DestroyPlayer(float delay) // Uncomment if you want to destroy the player
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    IEnumerator PauseAndShowMenu()
    {
        yield return new WaitForSeconds(pauseDelay);

        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
    }

    IEnumerator ShowText(string fullText)
    {
        textDisplay.text = ""; // Очистка текстового поля перед новым текстом
        int i = 0;
        while (i < fullText.Length)
        {
            textDisplay.text += fullText[i];
            yield return new WaitForSeconds(textSpeed);
            i++;
        }
        yield return new WaitForSeconds(2.0f); // Ждем 2 секунды
        textDisplay.text = ""; // Очистка текстового поля после 2 секунд
    }

}