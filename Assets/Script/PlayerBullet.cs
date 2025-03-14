using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private GameObject bloodPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
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
                GameObject blood = Instantiate(bloodPrefab, collision.transform.position, Quaternion.identity);
                Destroy(blood, 1f);
            }
            Destroy(gameObject);
        }
    }
}