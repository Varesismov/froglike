using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public float current_health = 100f;
    public float damage = 0f;
    public float armor = 2f;
    public float xpcarriedMin = 50f;
    public float xpcarriedMax = 100f;
    public string mobName = "Orc Warrior";

    public TextMeshProUGUI nameText;
    public GameObject xpPopupPrefab;


    public void Start()
    {
        if (nameText != null)
        {
            nameText.text = mobName;
        }
    }

    // Enemy taking damage
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
        float xpToGive = Random.Range(xpcarriedMin, xpcarriedMax);
        if (player != null)
        {
            player.GainXP(xpToGive);
        }

        if (xpPopupPrefab != null)
        {
            GameObject popup = Instantiate(xpPopupPrefab, transform.position, Quaternion.identity);

            XPTextPopup popupScript = popup.GetComponentInChildren<XPTextPopup>();
            if (popupScript != null)
            {
                popupScript.SetText("+" + xpToGive.ToString("F2") + " XP");
            }
        }


        Destroy(gameObject);
    }
}
