using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnStartAttack;

    private PlayerVisual playerVisual;
    private PlayerIdentity playerIdentity;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform attackPosition;

    private void Awake()
    {
        playerVisual = GetComponent<PlayerVisual>();
        playerIdentity = GetComponent<PlayerIdentity>();
    }
    public void InitializeAttackPosition()
    {
        //Top side player count is always = 2
        int maxPlayerInTopZone = 2;
        int playerSideIndex = playerIdentity.PlayerID % 2;
        var rightSideIndex = 0;

        var horizontalOffset = 1f;
        var downZoneOffsetY = 2f;
        var desiredPosition = Vector3.zero;

        if (playerIdentity.PlayerID < maxPlayerInTopZone)
        {
            //Players are in top zone
            if (playerSideIndex == rightSideIndex)
                desiredPosition.Set(horizontalOffset, 0f, 0f);
            else
                desiredPosition.Set(-horizontalOffset, 0f, 0f);
        }
        else
        {
            //Players are in down zone
            if (playerSideIndex == rightSideIndex)
                desiredPosition.Set(horizontalOffset, downZoneOffsetY, 0f);
            else
                desiredPosition.Set(-horizontalOffset, downZoneOffsetY, 0f);
        }

        attackPosition.transform.position += desiredPosition;
    }
    public IEnumerator HandleAttackSequence()
    {
        var currentPlayer = TurnManager.Instance.GetCurrentPlayer();
        var targetPlayer = TurnManager.Instance.GetTargetPlayer();
        Debug.Log(currentPlayer.PlayerName + " is attacking to " + targetPlayer);
        yield return new WaitForSeconds(1f);
        OnStartAttack?.Invoke();
        yield return new WaitForSeconds(2.2f);
        var bullet = Instantiate(bulletPrefab);
        bullet.name = currentPlayer.PlayerName + " bullet";
        bullet.GetComponent<Bullet>().InitializeBullet(currentPlayer.PlayerID,targetPlayer.transform.position, attackPosition.position, playerVisual.PlayerColor);
        yield return new WaitForSeconds(1f);
        StopCoroutine(nameof(HandleAttackSequence));
    }
    public void StartAttackSequence()
    {
        StartCoroutine(nameof(HandleAttackSequence));
    }
}
