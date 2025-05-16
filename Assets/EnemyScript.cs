using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Enemy took damage: " + dmg + ", remaining HP: " + health);

        if (health <= 0)
        {
            Die();
            Debug.Log("Enemy died.");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
