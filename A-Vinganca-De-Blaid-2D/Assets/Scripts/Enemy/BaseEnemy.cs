using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class BaseEnemy: MonoBehaviour
{
    protected Animator animator;
    protected Health health;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnHurt += PlayHurtAnim;
        health.OnDead += HandleDeath;
    }

    protected abstract void Update();

    private void PlayHurtAnim() => animator.SetTrigger("hurt");

    private void HandleDeath()
    {
        animator.SetTrigger("dead");
        StartCoroutine(DestroyEnemy(2));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}