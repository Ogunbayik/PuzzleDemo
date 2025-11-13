using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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
        var currentPlayer = GameManager.Instance.GetCurrentPlayer();
        var targetPlayer = GameManager.Instance.TargetPlayer;
        Debug.Log(currentPlayer.PlayerName + " is attacking to " + targetPlayer);
        yield return new WaitForSeconds(1f);
        Debug.Log(currentPlayer.PlayerName + " use attacking animation");
        yield return new WaitForSeconds(1f);
        var bullet = Instantiate(bulletPrefab);
        bullet.name = currentPlayer.PlayerName + " bullet";
        bullet.GetComponent<Bullet>().InitializeBullet(targetPlayer.transform.position, attackPosition.position, playerVisual.PlayerColor);
        yield return new WaitForSeconds(1f);
        Debug.Log("Happy");
        StopCoroutine(nameof(HandleAttackSequence));
    }
    public void StartAttacking()
    {
        StartCoroutine(nameof(HandleAttackSequence));
    }
}
