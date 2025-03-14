using UnityEngine;

public class Explosion_Skill : MonoBehaviour
{
    [SerializeField] private float damage = 150f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
        }
    }

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
