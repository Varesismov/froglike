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

    private Transform playerTransform;
    private EnemyMovement2D movement;


    public void Start()
    {
        if (nameText != null)
        {
            nameText.text = mobName;
        }
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found! Make sure it has tag 'Player'.");
        }

        movement = GetComponent<EnemyMovement2D>();
        if (movement == null)
        {
            Debug.LogError("EnemyMovement2D component missing!");
        }
    }
    void Update()
    {
        if (playerTransform != null && movement != null)
        {
            movement.SetTargetPosition(playerTransform.position);
        }

    }

    // --- Enemy Taking Damage ---
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


    // --- Enemy Death ---
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
