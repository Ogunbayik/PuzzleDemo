using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Action OnDead;

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
            Debug.Log(playerIdentity.PlayerName + " is dead.");
            currentHealth = 0;
            healthUI.UpdateHealthBar(currentHealth, maxHealth);
            OnDead?.Invoke();

            return;
        }

        currentHealth -= damage;
    }

}
