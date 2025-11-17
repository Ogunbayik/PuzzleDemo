using UnityEngine;

public class Bullet : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Vector3 movePosition;

    private int bulletID;
    private float bulletSpeed = 10f;
    private int bulletDamage = 50;
    public int BulletDamage => bulletDamage;
    public int BulletID => bulletID;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosition, bulletSpeed * Time.deltaTime);
        
    }
    public void InitializeBullet(int ID,Vector3 targetPosition, Vector3 spawnPosition, Color color)
    {
        bulletID = ID;
        movePosition = targetPosition;
        transform.position = spawnPosition;
        meshRenderer.material.color = color;
    }
    public void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}
