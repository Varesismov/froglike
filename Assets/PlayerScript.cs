using UnityEngine;
using UnityEngine.InputSystem;

public class Playerscript : MonoBehaviour
{
    // ---  Components  ---
    public Rigidbody2D myRb;
    public InputSystem_Actions playerControls;
    public GameObject projectilePrefab;
    public Sprite yourSprite;
    public Transform firePoint;

    // ---  Player Stats  ---
    public float damage = 10f;
    public float bulletSpeed = 10f;
    public float fireRate = 0.3f;
    public float moveSpeed = 5f;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public float armor = 1f;
    public float currentXP = 0f;
    public float playerLevel = 0;
    public int currentRunEnemiesKilled = 0;
    public float currentRunXp = 0f;


    // ---  Player interaction  ---
    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction attack;
    private InputAction attackDirection;
    Vector2 attackDir = Vector2.zero;
    private float lastFireTime = 0f;
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        attackDir = attackDirection.ReadValue<Vector2>();

        if (attackDir != Vector2.zero && Time.time - lastFireTime >= fireRate)
        {
            Shoot(attackDir);
            lastFireTime = Time.time;
        }
        firePoint.localPosition = attackDir.normalized * 0.3f;

    }
    private void FixedUpdate()
    {
        myRb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    // *******************************************************************
    // *                         Player Controls                         *
    // ******************************************************************* 

    // enabling every needed control
    private void OnEnable()
    {
        playerControls.Player.Enable();
        move = playerControls.Player.Move;
        move.Enable();

        attack = playerControls.Player.Attack;
        attack.Enable();
        //attack.performed += Attack;

        attackDirection = playerControls.Player.AttackDirection;
        attackDirection.Enable();
        //attackDirection.performed += AttackDirection;

    }
    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
        attackDirection.Disable();
    }

    // *******************************************************************
    // *                     Player Damage                               *
    // ******************************************************************* 

    // --- Player directional shooting ability ---
    void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * bulletSpeed;
        }

        SpriteRenderer sr = projectile.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = yourSprite;
        }
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.damage = this.damage * 2f;
        }
    }

    // --- Player taking damage --- 
    public void TakeDamage(float dmg)
    {
        currentHealth = currentHealth - dmg / armor;
        Debug.Log("You've taken" + dmg + " Damage. Your remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            Debug.Log("Player has died.");
        }
    }

    // *******************************************************************
    // *                         Player Death                            *
    // ******************************************************************* 
    public void Die() 
    {
        // GAME MODE CHANGE
        //float xpLoss = GameModeScript.Instance.GetXpLossOnDeath();
        //{
        //    currentXP -= currentXP * xpLoss;
        //    if (currentXP < 0) currentXP = 0;
        //}
        OnPlayerDeath();

        //Destroy(gameObject);
    }
    // *******************************************************************
    // *                         Progression Logic                       *
    // ******************************************************************* 
    void OnPlayerDeath()
    {
        ProgressionManager.Instance.data.totalEnemiesKilled += currentRunEnemiesKilled;
        ProgressionManager.Instance.data.totalXpCollected += currentRunXp;
        //ProgressionManager.Instance.data.totalRunsCompleted += 1;

        // np. odblokowywanie czegoœ
        if (ProgressionManager.Instance.data.totalEnemiesKilled >= 5)
        {
            ProgressionManager.Instance.data.unlockedItems.Add(1);
        }

        ProgressionManager.Instance.SaveProgress();
        currentRunEnemiesKilled = 0;
        currentRunXp = 0f;
    }


    //private void AttackDirection(InputAction.CallbackContext context)
    //{
    //    Debug.Log("You've succefully fired");
    //}

    // *******************************************************************
    // *                         XP Logic                                *
    // ******************************************************************* 
    public void GainXP(float xpGained)
    {
        currentXP += xpGained;
        Debug.Log($"+{xpGained} XP gained! Current XP: {currentXP}");

        currentRunXp += currentXP;

        float xpForNextLevel = 100f + playerLevel * 50f;

        if (currentXP >= xpForNextLevel)
        {
            currentXP -= xpForNextLevel;
            playerLevel++;
            Debug.Log($"Level up: {playerLevel}");
        }
    }

    // *******************************************************************
    // *                        Physics fix                              *
    // ******************************************************************* 

    // Getting rid of unwanted spining of a player (idk)
    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        if (myRb != null)
        {
            myRb.freezeRotation = true;
        }

    }

}
