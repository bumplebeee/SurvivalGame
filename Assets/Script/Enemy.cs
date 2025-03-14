using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 1f;
    protected Player player;
    [SerializeField] protected float MaxHp = 50f;
    protected float CurrentHp;
    [SerializeField] protected Image HpBar;
    [SerializeField] private float maxDistanceFromPlayer = 30f;
    
    protected EnemyGenerator enemyGenerator;
    protected Animator animator;
    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        enemyGenerator = FindAnyObjectByType<EnemyGenerator>();
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);  
        CurrentHp = MaxHp;
        UpdateHpBar();
    }
    protected virtual void Update()
    {
        moveToPlayer();
        CheckDistanceAndReset();
    }
    protected void moveToPlayer()
    {
        if (player != null && this != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
    protected void FlipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHp -= damage;
        CurrentHp = Mathf.Max(CurrentHp, 0);
        UpdateHpBar();
        if (CurrentHp <= 0)
        {
            Die();
        }
    }
    protected void UpdateHpBar()
    {
        if (HpBar != null)
        {
            HpBar.fillAmount = CurrentHp/MaxHp;
        }
    }
    protected virtual void Die()
    {
        if (enemyGenerator != null)
        {
            animator.SetBool("isDead", true);
            enemyMoveSpeed = 0;
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
            StartCoroutine(ReturnToPoolAfterDelay());
        }
    }
    public virtual void ResetEnemy()
    {
        enemyMoveSpeed = 1f;
        CurrentHp = MaxHp;
        UpdateHpBar();
    }
    public void SetEnemyGenerator(EnemyGenerator generator)
    {
        enemyGenerator = generator;
    }
    public virtual IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (enemyGenerator != null)
        {
            enemyGenerator.ReturnToPool(gameObject); 
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = true;
        }
    }
    private void CheckDistanceAndReset()
    {
        if (player != null && enemyGenerator != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance > maxDistanceFromPlayer)
            {
                transform.position = enemyGenerator.transform.position;
            }
        }
    }
}
