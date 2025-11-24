using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject spawnVFX;
    [SerializeField] private GameObject shieldVFX;
    [SerializeField] private GameObject hitVFX;
    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }
    public void PlayExplosionVFX(Vector3 spawnPosition)
    {
        var explosionEffect = Instantiate(explosionVFX);
        explosionEffect.transform.position = spawnPosition;
    }
    public void PlaySpawnVFX(Vector3 spawnPosition)
    {
        var spawnEffect = Instantiate(spawnVFX);
        spawnEffect.transform.position = spawnPosition;
    }
    public void PlayShieldVFX(Vector3 spawnPosition)
    {
        var shieldEffect = Instantiate(shieldVFX);
        shieldEffect.transform.position = spawnPosition;
    }
    public void PlayHitVFX(Vector3 spawnPosition)
    {
        var hitEffect = Instantiate(hitVFX);
        hitEffect.transform.position = spawnPosition;
    }
}
