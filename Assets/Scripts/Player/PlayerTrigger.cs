using System.Collections;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private PlayerIdentity playerIdentity;
    private PlayerHealth playerHealth;
    private HealthUI healthUI;
    private void Awake()
    {
        playerIdentity = GetComponent<PlayerIdentity>();
        playerHealth = GetComponent<PlayerHealth>();
        healthUI = GetComponentInChildren<HealthUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet.BulletID == playerIdentity.PlayerID)
            return;

        if (bullet != null)
            StartBulletTriggerSequnce(bullet);
    }
    private IEnumerator BulletTriggerSequence(Bullet bullet)
    {
        int damageAmount = bullet.BulletDamage;
        playerHealth.TakeDamage(damageAmount);
        bullet.DestroyPrefab();
        //Bullet için patlama efekti ekle
        yield return new WaitForSeconds(1f);
        healthUI.UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        yield return new WaitForSeconds(2f);
        TurnManager.Instance.AdvanceTurn();
        BoardManager.Instance.ResetSelectCount();
    }

    private void StartBulletTriggerSequnce(Bullet bullet)
    {
        StartCoroutine(BulletTriggerSequence(bullet));
    }
}
