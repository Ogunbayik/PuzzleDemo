using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Action<PlayerTrigger,Bullet> OnBulletTriggered;

    private PlayerHealth playerHealth;
    private HealthUI healthUI;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        healthUI = GetComponentInChildren<HealthUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            playerHealth.TakeDamage(bullet.BulletDamage);
            healthUI.UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
            bullet.gameObject.SetActive(false);
            GameManager.Instance.ChangePlayerTurn();
            BoardManager.Instance.ResetSelectCount();
        }
    }
}
