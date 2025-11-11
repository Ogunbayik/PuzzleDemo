using UnityEngine;

public class Bullet : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void InitializeBullet(Vector3 spawnPosition, Color color)
    {
        transform.position = spawnPosition;
        meshRenderer.material.color = color;
    }
}
