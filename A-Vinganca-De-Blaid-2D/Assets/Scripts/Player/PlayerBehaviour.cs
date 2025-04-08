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
        float moveDirection = GameManager.Instance.inputManager.Movement * Time.deltaTime * moveSpeed;
        transform.Translate(moveDirection, 0, 0);
    }

    private void HandleJump()
    {
        if (isGroundedChecker.IsGrounded())            
            _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }
}
