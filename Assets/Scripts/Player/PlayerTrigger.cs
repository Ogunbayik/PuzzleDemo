using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
public class PlayerTrigger : MonoBehaviour
{
    public event Action<Bullet> OnHitBullet;

    private PlayerIdentity playerIdentity;
    private void Awake()
    {
        playerIdentity = GetComponent<PlayerIdentity>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
            OnHitBullet?.Invoke(bullet);
    }

    private void OnEnable()
    {
        OnHitBullet += PlayerTrigger_OnHitBullet;
    }
    private void OnDisable()
    {
        OnHitBullet -= PlayerTrigger_OnHitBullet;
    }
    private void PlayerTrigger_OnHitBullet(Bullet bullet)
    {
        VFXManager.Instance.PlayHitVFX(bullet.transform.position);
        bullet.DestroyPrefab();
    }


}
