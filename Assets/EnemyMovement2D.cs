using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stoppingDistance = 0.3f;
    private Vector3 targetPosition;
    private bool hasTarget = false;


    void Update()
    {
        if (!hasTarget) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance)
        {
            hasTarget = false;
        }

    }

    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        hasTarget = true;
    }
}
