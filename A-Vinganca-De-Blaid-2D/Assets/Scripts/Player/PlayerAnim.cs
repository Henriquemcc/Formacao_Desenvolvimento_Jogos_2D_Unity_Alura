using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker isGroundedChecker;
    private Health playerHealth;

    private void PlayerHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    private void PlayerDeadAnim()
    {
        animator.SetTrigger("dead");
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<Health>();

        playerHealth.OnHurt += PlayerHurtAnim;
        playerHealth.OnDead += PlayerDeadAnim;
    }

    void Update()
    {
        bool isMoving = GameManager.Instance.inputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);
        bool isJumping = !isGroundedChecker.IsGrounded();
        animator.SetBool("isJumping", isJumping);
    }
}
