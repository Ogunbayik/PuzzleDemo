using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerIdentity playerIdentity;
    private PlayerVisual playerVisual;
    private HealthUI healthUI;

    [SerializeField] private int maxHealth;

    private int currentHealth;
    private void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
        playerIdentity = GetComponent<PlayerIdentity>();
        playerVisual = GetComponent<PlayerVisual>();
    }
    public void InitializeHealthBar(Vector3 offset)
    {
        healthUI.InitializeBar(offset, playerVisual.PlayerColor, playerVisual.FrameColor);
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > damage)
            currentHealth -= damage;
        else
            currentHealth = 0;
    }
}
