using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stoppingDistance = 0.3f;

    private Vector3 targetPosition;
    private bool hasTarget = false;

    // --- References to objects ---
    private Rigidbody2D rb;
    private EnemyScript enemyScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<EnemyScript>();
        if (enemyScript == null)
        {
            Debug.LogError("EnemyScript component missing!");
        }
    }

    void FixedUpdate()
    {
        if (!hasTarget) return;

        Vector2 currentPos = rb.position;
        Vector2 targetPos = new Vector2(targetPosition.x, targetPosition.y);

        Vector2 newPos = Vector2.MoveTowards(currentPos, targetPos, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        hasTarget = true;
    }
    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            //hasTarget = false; // Stopping movement

            Playerscript player = trigger.gameObject.GetComponent<Playerscript>();
            if (player != null && enemyScript != null)
            {
                enemyScript.UpdateContactDamage(player, Time.deltaTime);
                //enemyScript.DealDamageToPlayer(player);
            }
        }
    }


}
