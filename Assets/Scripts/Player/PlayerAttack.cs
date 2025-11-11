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
            desiredPosition.Set(offsetX, offsetY, 0f);
        else
            desiredPosition.Set(-offsetX, offsetY, 0f);

        attackPosition.transform.position += desiredPosition;
    }
    private void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        var bulletColor = playerVisual.PlayerColor;
        bullet.GetComponent<Bullet>().InitializeBullet(transform.position, bulletColor);
    }
}
