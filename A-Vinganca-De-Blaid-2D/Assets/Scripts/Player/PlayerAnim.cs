using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker isGroundedChecker;
    private Health playerHealth;

    void Awake()
    {
        animator = GetComponent<Animator>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<Health>();

        playerHealth.OnHurt += PlayHurtAnim;
        playerHealth.OnDead += PlayDeadAnim;
        GameManager.Instance.inputManager.OnAttack += PlayAttackAnim;
    }

    void Update()
    {
        bool isMoving = GameManager.Instance.inputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);
        bool isJumping = !isGroundedChecker.IsGrounded();
        animator.SetBool("isJumping", isJumping);
    }

    private void PlayAttackAnim()
    {
        animator.SetTrigger("attack");
    }

    private void PlayHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    private void PlayDeadAnim()
    {
        animator.SetTrigger("dead");
    }
}
