using UnityEngine;

public class Bullet : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Vector3 movePosition;

    private float bulletSpeed = 10f;
    private int bulletDamage;
    public int BulletDamage => bulletDamage;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        bulletDamage = Consts.GameDamage.FIREBALL_DAMAGE;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosition, bulletSpeed * Time.deltaTime);
    }
    public void InitializeBullet(Transform movementPos, Vector3 spawnPosition,Color color)
    {
        movePosition = movementPos.position;
        transform.position = spawnPosition;
        meshRenderer.material.color = color;
    }
    public void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}
