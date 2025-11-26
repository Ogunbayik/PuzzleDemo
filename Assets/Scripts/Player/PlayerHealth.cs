using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDead;
    public event Action OnHit;

    private PlayerIdentity playerIdentity;
    private PlayerTrigger playerTrigger;
    private PlayerVisual playerVisual;
    private HealthUI healthUI;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth;

    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    private bool isInvulnerable;
    private void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
        playerIdentity = GetComponent<PlayerIdentity>();
        playerVisual = GetComponent<PlayerVisual>();
        playerTrigger = GetComponent<PlayerTrigger>();
    }
    public void InitializeHealthBar(Vector3 offset)
    {
        healthUI.InitializeBar(offset, playerVisual.PlayerColor, playerVisual.FrameColor);
    }
    private void OnEnable()
    {
        playerTrigger.OnHitBullet += PlayerTrigger_OnHitBullet;
        BoardManager.Instance.OnClickBomb += Instance_OnClickBomb;
    }
    private void OnDisable()
    {
        playerTrigger.OnHitBullet -= PlayerTrigger_OnHitBullet;
        BoardManager.Instance.OnClickBomb -= Instance_OnClickBomb;
    }
    private void Instance_OnClickBomb(PlayerIdentity currentPlayer)
    {
        if (currentPlayer.PlayerID == playerIdentity.PlayerID)
            TakeDamage(Consts.GameDamage.BOMB_DAMAGE);
    }
    private void PlayerTrigger_OnHitBullet(Bullet bullet)
    {
        int damageMultiply = TurnManager.Instance.CanDoubleDamage() ? Consts.GameDamage.DOUBLE_DAMAGE_MULTIPLIER : Consts.GameDamage.DEFAULT_MULTIPLIER;
        TakeDamage(bullet.BulletDamage * damageMultiply);
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
            currentHealth -= damage;
        else
        {
            HandleInvulnerableSequence();
            return;
        }

        if (currentHealth <= 0)
            HandleDeadSequence();
        else
            HandleHitSequence();
    }
    private void HandleInvulnerableSequence()
    {
        Sequence invulnerableSequence = DOTween.Sequence();

        invulnerableSequence.AppendInterval(Consts.DelayTime.ADVANCE_TURN_DELAY);
        invulnerableSequence.AppendCallback(() => TurnManager.Instance.AdvanceTurn());
        invulnerableSequence.JoinCallback(() => BoardManager.Instance.ResetSelectedTiles());
    }
    private void HandleHitSequence()
    {
        Sequence hitSequence = DOTween.Sequence();
        //First Part
        hitSequence.AppendInterval(Consts.PlayerAnimationTime.HIT_ANIMATION_DURATION);
        hitSequence.AppendCallback(() => healthUI.HandleHealthChange(currentHealth, maxHealth, Consts.DelayTime.REMAINFILL_DECREASE_DELAY));
        //SecondPart
        hitSequence.AppendInterval(Consts.DelayTime.ADVANCE_TURN_DELAY);
        hitSequence.AppendCallback(() => TurnManager.Instance.AdvanceTurn());
        hitSequence.JoinCallback(() => TurnManager.Instance.SetDoubleDamageState(false));
        hitSequence.JoinCallback(() => BoardManager.Instance.ResetSelectedTiles());
    }
    private void HandleDeadSequence()
    {
        Sequence deadSequence = DOTween.Sequence();

        //First Part
        deadSequence.AppendInterval(Consts.PlayerAnimationTime.HIT_ANIMATION_DURATION);
        deadSequence.AppendCallback(() => CameraManager.Instance.SetHelperCameraPosition(this.transform.position));
        deadSequence.JoinCallback(() => CameraManager.Instance.ToggleGameCamera());
        //Second Part
        deadSequence.AppendInterval(Consts.DelayTime.PLAYER_HEALTH_CHANGE_DELAY);
        deadSequence.AppendCallback(() => currentHealth = 0);
        deadSequence.JoinCallback(() => healthUI.HandleHealthChange(currentHealth, maxHealth, Consts.DelayTime.REMAINFILL_DECREASE_DELAY));
        //Third Part
        deadSequence.AppendInterval(Consts.DelayTime.START_PLAYER_DEAD_DELAY);
        deadSequence.AppendCallback(() => OnDead?.Invoke());
        deadSequence.JoinCallback(() => TurnManager.Instance.RemoveDeadPlayer(playerIdentity));
        //Last Part
        deadSequence.AppendInterval(Consts.PlayerAnimationTime.DEAD_ANIMATION_DURATION);
        deadSequence.AppendCallback(() => CameraManager.Instance.ToggleGameCamera());
        //Last Part
        deadSequence.AppendInterval(Consts.DelayTime.ADVANCE_TURN_DELAY);
        deadSequence.AppendCallback(() => TurnManager.Instance.AdvanceTurn());
        deadSequence.JoinCallback(() => TurnManager.Instance.SetDoubleDamageState(false));
        deadSequence.JoinCallback(() => BoardManager.Instance.ResetSelectedTiles());
    }
    public void SetInvulnerableStatue(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }
}
