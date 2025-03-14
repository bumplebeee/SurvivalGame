using UnityEngine;

public class PlayerSkill1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private GameObject explosionPrefab;

    public static float cooldown = 10f;
    public static float cooldownTimer = 0f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        MoveBullet();

    }

    void MoveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
            }
            Destroy(gameObject);
        }
    }

    public static bool CanUseSkill()
    {
        return cooldownTimer <= 0;
    }

    public static void ActivateSkill()
    {
        if (CanUseSkill())
        {
            cooldownTimer = cooldown;
        }
    }
}
