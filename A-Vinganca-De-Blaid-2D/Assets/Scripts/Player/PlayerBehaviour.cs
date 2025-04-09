using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private float jumpForce = 3;

    private Rigidbody2D _rigidbody;
    private IsGroundedChecker isGroundedChecker;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
    }

    private void Start()
    {
        GameManager.Instance.inputManager.OnJump += HandleJump;
    }

    private void Update()
    {
        // Realiza a movimentação horizontal do personagem
        float moveDirection = GameManager.Instance.inputManager.Movement;
        _rigidbody.linearVelocity = new Vector2(moveDirection * moveSpeed, _rigidbody.linearVelocityY);

        // Realiza a mudança de direção do personagem
        if (moveDirection < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (moveDirection > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Realiza o pulo do personagem
    private void HandleJump()
    {
        if (isGroundedChecker.IsGrounded())            
            _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }
}
