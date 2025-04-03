using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        _rigidbody.linearVelocity += Vector2.up;
    }
}
