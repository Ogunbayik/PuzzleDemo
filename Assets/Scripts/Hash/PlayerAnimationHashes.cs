using UnityEngine;

public static class PlayerAnimationHashes
{
    public static readonly int IS_DEAD_HASH = Animator.StringToHash("OnDead");
    public static readonly int IS_ATTACK_HASH = Animator.StringToHash("OnAttack");
    public static readonly int IS_HIT_HASH = Animator.StringToHash("OnHit");
    public static readonly int IS_ACTIVEIDLE_HASH = Animator.StringToHash("OnActive");
    public static readonly int IS_PASSIVEIDLE_HASH = Animator.StringToHash("OnPassive");
}
