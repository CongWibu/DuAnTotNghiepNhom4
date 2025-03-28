using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int maxHealth = 3;
    private int currentHealth;
    public List<Image> healthIcons;
    public string gameOverScene;
    private bool isInvincible = false; // Trạng thái bất tử
    public float invincibilityDuration = 3f; // Thời gian bất tử
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) // Nếu đang bất tử thì không nhận sát thương
            return;

        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(gameOverScene);
        }
        else
        {
            StartCoroutine(InvincibilityTimer()); // Kích hoạt bất tử 3 giây sau khi mất máu
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            healthIcons[i].enabled = i < currentHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
            StartCoroutine(InvincibilityTimer());
        }
    }

    IEnumerator InvincibilityTimer()
    {
        isInvincible = true;

        // Nhấp nháy để báo hiệu bất tử
        for (float i = 0; i < invincibilityDuration; i += 0.5f) // Nhấp nháy trong thời gian invincibilityDuration
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Bật/Tắt sprite
            yield return new WaitForSeconds(0.5f); // Mỗi lần nhấp nháy mất 0.5s
        }

        spriteRenderer.enabled = true; // Đảm bảo nhân vật hiện lại sau bất tử
        isInvincible = false; // Kết thúc thời gian bất tử
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
