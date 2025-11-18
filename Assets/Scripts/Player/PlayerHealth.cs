using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDead;
    public event Action OnHit;

    private PlayerTrigger playerTrigger;
    private PlayerIdentity playerIdentity;
    private PlayerVisual playerVisual;
    private HealthUI healthUI;

    [SerializeField] private int maxHealth;

    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    private void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
        playerTrigger = GetComponent<PlayerTrigger>();
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
        if(currentHealth <= damage)
        {
            currentHealth = 0;
            healthUI.UpdateHealthBar(currentHealth, maxHealth);
            TurnManager.Instance.RemoveDeadPlayer(playerIdentity);

            OnDead?.Invoke();

            return;
        }

        currentHealth -= damage;
        OnHit?.Invoke();
    }
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
