using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown;

    [Header("Audio properties")]
    [SerializeField] private AudioClip[] audioClips;

    private float cooldownTimer;

    protected override void Awake()
    {
        base.Awake();
        base.health.OnHurt += PlayHurtAudio;
        base.health.OnDead += PlayDeadAudio;
    }

    protected override void Update()
    {
        cooldownTimer += Time.deltaTime;
        VerifyCanAttack();
    }

    private void VerifyCanAttack()
    {
        if (cooldownTimer < attackCooldown) return;
        if (PlayerInSight())
        {
            animator.SetTrigger("attack");
            AttackPlayer();
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private void AttackPlayer()
    {
        audioSource.clip = audioClips[0];
        cooldownTimer = 0;
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            print("Making player take damage");
            playerHealth.TakeDamage();
        }
    }

    private bool PlayerInSight()
    {
        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;
    }

    private void OnDrawGizmos()
    {
        if (detectPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
        }
    }

    private void PlayHurtAudio()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    private void PlayDeadAudio()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}
