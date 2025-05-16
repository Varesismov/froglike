using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public float current_health = 100f;
    public float damage = 0f;
    public float armor = 0f;
    public float xpcarried = 100f;
    public string mobName = "Orc Warrior";

    public TextMeshProUGUI nameText;

    public void Start()
    {
        if (nameText != null)
        {
            nameText.text = mobName;
        }
    }

    public void TakeDamage(float dmg)
    {
        current_health = current_health - dmg / armor;
        Debug.Log("Enemy took damage: " + dmg + ", remaining HP: " + current_health);

        if (current_health <= 0)
        {
            Die();
            Debug.Log("Enemy died.");
        }
    }

    private void Die()
    {
        Playerscript player = FindFirstObjectByType<Playerscript>();

        if (player != null)
        {
            player.GainXP(xpcarried);
        }

        Destroy(gameObject);
    }
}
