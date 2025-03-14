using UnityEngine;

public class PlayerCollidor : MonoBehaviour
{
    [SerializeField]
    private GameObjectSc GameObjectSc;

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Energy"))
        {
            GameObjectSc.AddEneyy();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            Player player= GetComponent<Player>();
            player.TakeDamage(5f);
        }
    }

}
