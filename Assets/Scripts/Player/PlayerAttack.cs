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
        var index = playerIdentity.PlayerID % 2;
        var offsetX = 1;
        var offsetY = 1;
        var desiredPosition = Vector3.zero;

        if (index == 0)
            desiredPosition.Set(-offsetX, offsetY, 0f);
        else
            desiredPosition.Set(offsetX, offsetY, 0f);

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
        var bulletColor = playerVisual.PlayerColor;
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
