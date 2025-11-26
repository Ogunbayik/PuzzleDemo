using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnStartAttack;

    private PlayerIdentity playerIdentity;
    private PlayerVisual playerVisual;

    [Header("Attack Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform attackPosition;
    private void Awake()
    {
        playerIdentity = GetComponent<PlayerIdentity>();
        playerVisual = GetComponent<PlayerVisual>();
    }
    private void OnEnable()
    {
        GameUIManager.Instance.OnEnemyTargetSelected += Instance_OnEnemyTargetSelected;
    }
    private void OnDisable()
    {
        GameUIManager.Instance.OnEnemyTargetSelected -= Instance_OnEnemyTargetSelected;
    }
    private void Instance_OnEnemyTargetSelected(PlayerIdentity target)
    {
        if (playerIdentity.PlayerID == TurnManager.Instance.currentPlayerIndex)
            HandleAttackSequence(target);
    }
    private void HandleAttackSequence(PlayerIdentity target)
    {
        Sequence attackSequnce = DOTween.Sequence();
        attackSequnce.Append(playerVisual.bodyVisual.transform.DOLookAt(target.transform.position, Consts.DelayTime.PLAYER_LOOK_TIME));
        attackSequnce.JoinCallback(() => GameUIManager.Instance.HideTargetPanel());
        attackSequnce.JoinCallback(() => GameUIManager.Instance.ResetTargetButton());
        //First Part
        var randomPlayer = TurnManager.Instance.GetRandomPlayer();
        attackSequnce.AppendInterval(Consts.DelayTime.ACTIVATE_SHIELD_DELAY);
        attackSequnce.AppendCallback(() => randomPlayer.GetComponent<PlayerVisual>().ActivateShield());
        attackSequnce.JoinCallback(() => OnStartAttack?.Invoke());
        //Second Part
        attackSequnce.AppendInterval(Consts.PlayerAnimationTime.ATTACK_ANIMATION_DURATION);
        attackSequnce.AppendCallback(() => CreateBullet(target));
    }
    private void CreateBullet(PlayerIdentity target)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().InitializeBullet(target.transform, attackPosition.position, playerVisual.PlayerColor);
    }
}
