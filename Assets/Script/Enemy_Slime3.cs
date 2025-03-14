using UnityEngine;

public class Enemy_Slime3 : Enemy
{
    private float lastDamageTime = 0f;
    [SerializeField] private float damageInterval = 0.5f;
    [SerializeField] private GameObject explosionPrefab;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= lastDamageTime + damageInterval)
        {
            player.TakeDamage(damageInterval);
            lastDamageTime = Time.time;
        }
    }
    private void CreateExplosion()
    {
        if (explosionPrefab != null) { 
        Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        }
    }
    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }
}
