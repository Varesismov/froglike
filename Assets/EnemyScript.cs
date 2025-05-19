using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public EnemyData data; // <-- new data source

    
    //public float xpcarriedMin = 50f;
    //public float xpcarriedMax = 100f;
    //public string mobName = "Orc Warrior";
    //public float contactAttackCooldown = 1.0f;
    //public float damageIncreaseInterval = 1f;
    //public float damageIncreaseAmount = 5f;

    // --- References to objects ---
    public TextMeshProUGUI nameText;
    public GameObject xpPopupPrefab;
    public SpriteRenderer spriteRenderer;

    private Transform playerTransform;
    private EnemyMovement2D movement;


    private float lastContactAttackTime = -999f;
    private float timeInContact = 0f;
    private float baseDamage;
    private float current_health;
    private float damage;
    private float armor;


    private void Start()
    {
        if (data == null)
        {
            Debug.LogError("EnemyData is missing on " + gameObject.name);
            return;
        }

        current_health = data.maxHealth;
        baseDamage = data.baseDamage;
        damage = baseDamage;

        if (nameText != null)
        {
            nameText.text = data.mobName;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTransform = playerObj.transform;

        movement = GetComponent<EnemyMovement2D>();

        if (spriteRenderer != null && data.icon != null)
        {
            spriteRenderer.sprite = data.icon;
        }
    }
    private void Update()
    {
        if (playerTransform != null && movement != null)
            movement.SetTargetPosition(playerTransform.position);
    }


    // *******************************************************************
    // *                         Enemy Damage                            *
    // ******************************************************************* 

    // --- Contanct Damage update ---
    //Changing damage value over time spent being in contact with a player
    public void UpdateContactDamage(Playerscript player, float deltaTime)
    {
        timeInContact += deltaTime;

        int multiplier = Mathf.FloorToInt(timeInContact / data.damageIncreaseInterval);
        damage = baseDamage + (multiplier * data.damageIncreaseAmount);

        DealDamageToPlayer(player);
    }

    // --- Dealing Contact Damage logic ---
    public void DealDamageToPlayer(Playerscript player)
    {
        if (Time.time - lastContactAttackTime < data.contactAttackCooldown)
            return;

        lastContactAttackTime = Time.time;
        if (player != null) player.TakeDamage(damage);
    }

    // --- Enemy Taking Damage ---
    public void TakeDamage(float dmg)
    {
        current_health -= dmg / data.armor;
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
        float xpToGive = Random.Range(data.xpcarriedMin, data.xpcarriedMax);

        if (player != null)
        {
            player.GainXP(xpToGive);
            player.currentRunEnemiesKilled += 1;
        }

        if (xpPopupPrefab != null)
        {
            GameObject popup = Instantiate(xpPopupPrefab, transform.position, Quaternion.identity);
            popup.GetComponentInChildren<XPTextPopup>()?.SetText("+" + xpToGive.ToString("F2") + " XP");
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
