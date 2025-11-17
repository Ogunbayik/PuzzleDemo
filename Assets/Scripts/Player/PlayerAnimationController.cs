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
        animator.SetTrigger(Consts.PlayerAnimationParameter.HIT_PARAMETER);
    }
    public void PlayAttackAnimation()
    {
        animator.SetTrigger(Consts.PlayerAnimationParameter.ATTACK_PARAMETER);
    }
    public void PlayDeadAnimation()
    {
        animator.SetTrigger(Consts.PlayerAnimationParameter.DEAD_PARAMETER);
    }
    public void PlayTurnIdleAnimation()
    {
        animator.SetBool("IsPlayerTurn", true);
    }
    public void PlaySleepIdleAnimation()
    {
        animator.SetBool("IsPlayerTurn", false);
    }

}
