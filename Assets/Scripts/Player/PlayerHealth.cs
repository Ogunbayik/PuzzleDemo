using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDead;
    public event Action OnHit;

    private PlayerIdentity playerIdentity;
    private PlayerVisual playerVisual;
    private HealthUI healthUI;

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
    }
    public void InitializeHealthBar(Vector3 offset)
    {
        healthUI.InitializeBar(offset, playerVisual.PlayerColor, playerVisual.FrameColor);
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            OnHit?.Invoke();
        }
        else
            return;

        if (currentHealth <= 0)
            StartCoroutine(nameof(HandleDeadSequence));
    }
    public IEnumerator HandleDeadSequence()
    {
        yield return new WaitForSeconds(Consts.DelayTime.PLAYER_HIT_ANIMATION_DURATION);
        CameraManager.Instance.SetHelperCameraPosition(this.transform.position);
        CameraManager.Instance.ToggleGameCamera();
        yield return new WaitForSeconds(Consts.DelayTime.PLAYER_HEALTH_CHANGE_DELAY);
        currentHealth = 0;
        healthUI.HandleHealthChange(currentHealth, maxHealth, Consts.DelayTime.REMAINFILL_DECREASE_DELAY);
        TurnManager.Instance.RemoveDeadPlayer(playerIdentity);
        yield return new WaitForSeconds(Consts.DelayTime.START_PLAYER_DEAD_DELAY);
        OnDead?.Invoke();
        yield return new WaitForSeconds(Consts.DelayTime.PLAYER_DEAD_ANIMATION_DURATION);
        CameraManager.Instance.ToggleGameCamera();
    }
    public void SetInvulnerableStatue(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }
}
