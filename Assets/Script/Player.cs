using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 lastDirection = Vector2.right;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private float MaxHp = 50f;
    private float CurrentHp;
    private bool isDead = false;
    [SerializeField] private Image HpBar;
    [SerializeField] private Image CooldownBar;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int MessageBox(int hWnd, string text, string caption, int type);

    void Start()
    {
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        CurrentHp = MaxHp;

        if (CooldownBar != null)
        {
            CooldownBar.fillAmount = 0;
        }
    }

    void Update()
    {
        PlayerMovement();
        UpdateCooldownBar();
    }

    void PlayerMovement()
    {
        if (!isDead)
        {
            Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb.linearVelocity = playerInput.normalized * moveSpeed;

            if (playerInput != Vector2.zero)
            {
                lastDirection = playerInput;
            }

            spriteRenderer.flipX = lastDirection.x < 0;
            animator.SetBool("isRun", playerInput != Vector2.zero);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHp -= damage;
        CurrentHp = Mathf.Max(CurrentHp, 0);
        UpdateHpBar();

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        Invoke(nameof(GameOver), 1.5f);
    }

    private void UpdateHpBar()
    {
        if (HpBar != null)
        {
            HpBar.fillAmount = CurrentHp / MaxHp;
        }
    }

    public void Heal(float healValue)
    {
        if (CurrentHp < MaxHp)
        {
            CurrentHp += healValue;
            CurrentHp = Mathf.Min(CurrentHp, MaxHp);
            UpdateHpBar();
        }
    }

    public void GameOver()
    {
        int result = MessageBox(0, "Game Over! Try again?", "Game Over", 1);

        if (result == 1)
        {
            RestartGame();
        }
        else
        {
            Application.Quit();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateCooldownBar()
    {
        if (CooldownBar != null)
        {
            CooldownBar.fillAmount = 1 - (PlayerSkill1.cooldownTimer / 10f);
        }

    }
}
