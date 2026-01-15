using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    [Header("Ustawienia Ogólne")]
    public float moveSpeed = 3f;
    public int damageAmount = 10;

    [Header("Patrol (Lewo-Prawo)")]
    public float patrolRange = 3f;
    private Vector2 startPosition;
    private bool movingRight = true;

    [Header("Dystanse AI")]
    public float chaseRange = 5f;
    public float attackRange = 1f;

    [Header("Atak - Opóźnienie")]
    public float attackChargeTime = 1.0f;
    private float currentChargeTimer = 0f;

    [Header("Atak - Cooldown")]
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    [Header("Referencje")]
    public Transform playerTransform;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (playerTransform == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);


        if (distanceToPlayer <= attackRange)
        {
            PrepareAttack();
        }

        else if (distanceToPlayer <= chaseRange)
        {
            ResetAttackCharge();
            ChasePlayer();
        }
       
        else
        {
            ResetAttackCharge();
            Patrol();
        }
    }

  
    void PrepareAttack()
    {
        currentChargeTimer += Time.deltaTime;
    
        if (currentChargeTimer >= attackChargeTime)
        {
            TryDealDamage();
        }
    }

    void TryDealDamage()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                
            
                lastAttackTime = Time.time;
                currentChargeTimer = 0f;
            }
        }
    }

    void ResetAttackCharge()
    {
        currentChargeTimer = 0f;
    }

    void Patrol()
    {
        float targetX = movingRight ? (startPosition.x + patrolRange) : (startPosition.x - patrolRange);
        Vector2 targetPosition = new Vector2(targetX, startPosition.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Vector2 center = Application.isPlaying ? startPosition : (Vector2)transform.position;
        Gizmos.DrawLine(new Vector2(center.x - patrolRange, center.y), new Vector2(center.x + patrolRange, center.y));
    }
}