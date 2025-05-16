using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f;
    public float damage = 0f; 


    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit, damage dealt: " + damage);
            }

            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("TilemapCollision"))
        {
            Destroy(gameObject);
            Debug.Log("Projectile has hit an object.");
        }
    }
}
