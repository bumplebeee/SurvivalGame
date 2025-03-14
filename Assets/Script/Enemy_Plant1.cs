using UnityEngine;

public class Enemy_Plant1 : Enemy
{
    private float lastDamageTime = 0f;
    [SerializeField] private float damageInterval = 0.5f;

    [SerializeField] private float healValue = 10f;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(5);
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
    protected override void Die()
    {
        HealPlayer();
        
        base.Die();
    }
    private void HealPlayer()
    {
        if(player != null)
        {
             player.Heal(healValue);
        }
    }
}
