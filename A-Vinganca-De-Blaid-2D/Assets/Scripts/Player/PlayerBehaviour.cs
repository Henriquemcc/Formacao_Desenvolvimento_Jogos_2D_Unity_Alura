using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private float jumpForce = 3;

    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    private Rigidbody2D _rigidbody;
    private IsGroundedChecker isGroundedChecker;

    private float moveDirection;

    private void HandlePlayerDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.inputManager.DisablePlayerInput();
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        GetComponent<Health>().OnDead += HandlePlayerDeath;
    }

    private void Start()
    {
        GameManager.Instance.inputManager.OnJump += HandleJump;
    }

    // Realiza a movimentação horizontal do personagem
    private void MovePlayer()
    {
        moveDirection = GameManager.Instance.inputManager.Movement;
        _rigidbody.linearVelocity = new Vector2(moveDirection * moveSpeed, _rigidbody.linearVelocityY);
    }

    // Realiza a mudança de direção do personagem
    private void FlipSpriteAccordingToMoveDirection()
    {
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Update()
    {
        MovePlayer();
        FlipSpriteAccordingToMoveDirection();
    }

    // Realiza o pulo do personagem
    private void HandleJump()
    {
        if (isGroundedChecker.IsGrounded())            
            _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }

    private void Attack()
    {
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);
        print("Making enemy take damage");
        print(hittedEnemies.Length);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            print("Checking enemy");
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                print("Getting damage");
                enemyHealth.TakeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
