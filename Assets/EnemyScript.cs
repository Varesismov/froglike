using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public float current_health = 100f;
    public float damage = 10f;
    public float armor = 2f;
    public float xpcarriedMin = 50f;
    public float xpcarriedMax = 100f;
    public string mobName = "Orc Warrior";
    public float contactAttackCooldown = 1.0f;
    public float damageIncreaseInterval = 1f;
    public float damageIncreaseAmount = 5f;

    // --- References to objects ---
    public TextMeshProUGUI nameText;
    public GameObject xpPopupPrefab;

    private Transform playerTransform;
    private EnemyMovement2D movement;


    private float lastContactAttackTime = -999f;
    private float timeInContact = 0f;
    private float baseDamage;


    public void Start()
    {
        if (nameText != null)
        {
            nameText.text = mobName;
        }
        // Fiding target's position [transform] to chase. In this case a "Player"
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
        baseDamage = damage;
    }
    void Update()
    {
        if (playerTransform != null && movement != null)
        {
            movement.SetTargetPosition(playerTransform.position);
        }

    }
    // *******************************************************************
    // *                         Enemy Damage                            *
    // ******************************************************************* 

    // --- Contanct Damage update ---
    //Changing damage value over time spent being in contact with a player
    public void UpdateContactDamage(Playerscript player, float deltaTime)
    {
        timeInContact += deltaTime;

        // Damage increasing with every [damageIncreaseInterval] seconds.
        int multiplier = Mathf.FloorToInt(timeInContact / damageIncreaseInterval);
        damage = baseDamage + (multiplier * damageIncreaseAmount);

        DealDamageToPlayer(player);
    }

    // --- Dealing Contact Damage logic ---
    public void DealDamageToPlayer(Playerscript player)
    {
        if (Time.time - lastContactAttackTime < contactAttackCooldown)
            return;

        lastContactAttackTime = Time.time;

        if (player != null)
        {
            player.TakeDamage(damage);
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

    // *******************************************************************
    // *                         OnTrigger                               *
    // ******************************************************************* 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy is no longer dealing damage to you");
            // [TODO]: Anmiation end, target reset, etc.

            timeInContact = 0f;
            damage = baseDamage;
        }
    }
}
