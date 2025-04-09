using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker isGroundedChecker;

    void Awake()
    {
        animator = GetComponent<Animator>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
    }

    void Update()
    {
        bool isMoving = GameManager.Instance.inputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);
        bool isJumping = !isGroundedChecker.IsGrounded();
        animator.SetBool("isJumping", isJumping);
    }
}
