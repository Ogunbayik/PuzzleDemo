using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponentInParent<PlayerAttack>();
        playerHealth = GetComponentInParent<PlayerHealth>();
    }
    private void OnEnable()
    {
        playerHealth.OnDead += PlayerHealth_OnDead;
        playerHealth.OnHit += PlayerHealth_OnHit;
        playerAttack.OnStartAttack += PlayerAttack_OnStartAttack;
    }
    private void OnDisable()
    {
        playerHealth.OnDead -= PlayerHealth_OnDead;
        playerHealth.OnHit -= PlayerHealth_OnHit;
        playerAttack.OnStartAttack -= PlayerAttack_OnStartAttack;
    }
    private void PlayerAttack_OnStartAttack()
    {
        PlayAttackAnimation();
    }
    private void PlayerHealth_OnHit()
    {
        PlayHitAnimation();
    }
    private void PlayerHealth_OnDead()
    {
        PlayDeadAnimation();
    }
    public void PlayHitAnimation()
    {
        animator.SetTrigger(PlayerAnimationHashes.IS_HIT_HASH);
    }
    public void PlayAttackAnimation()
    {
        animator.SetTrigger(PlayerAnimationHashes.IS_ATTACK_HASH);
    }
    public void PlayDeadAnimation()
    {
        animator.SetTrigger(PlayerAnimationHashes.IS_DEAD_HASH);
    }
    public void PlayActiveIdleAnimation()
    {
        animator.SetTrigger(PlayerAnimationHashes.IS_ACTIVEIDLE_HASH);
    }
    public void PlayPassiveIdleAnimation()
    {
        animator.SetTrigger(PlayerAnimationHashes.IS_PASSIVEIDLE_HASH);
    }

}
