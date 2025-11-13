using System;
using System.Collections;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private PlayerVisual playerVisual;
    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;
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
            StartCoroutine(BulletTriggerSequence(bullet));
    }

    private IEnumerator BulletTriggerSequence(Bullet bullet)
    {
        Debug.Log("Play playerhit animation");
        playerHealth.TakeDamage(bullet.BulletDamage);
        bullet.DestroyPrefab();
        yield return new WaitForSeconds(1f);
        healthUI.UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        Debug.Log("Set target player healthUI with effect");
        yield return new WaitForSeconds(1f);
        Debug.Log("If target player is dead, activate player fall animation");
        Debug.Log("Use timeline to show player's fall");
        yield return new WaitForSeconds(2f);
        BoardManager.Instance.ResetSelectCount();
        GameManager.Instance.ChangePlayerTurn();
    }
}
