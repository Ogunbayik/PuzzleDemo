using System.Collections.Generic;
using UnityEngine;

public class HealthSystemUI : MonoBehaviour
{
    private Dictionary<int,HealthBar> activeHealthBars = new Dictionary<int,HealthBar>();

    [SerializeField] private GameObject healthBarPrefab;

    [SerializeField] private Color[] playerColors;
    [SerializeField] private Color[] playerFrameColors;

    public int playerCount;

    void Start()
    {
        for (int i = 0; i < playerCount; i++)
        {
            var playerColor = playerColors[i];
            var playerFrameColor = playerFrameColors[i];

            var barPrefab = Instantiate(healthBarPrefab, transform);
            var healthBar = barPrefab.GetComponent<HealthBar>();
            healthBar.SetupHealthBar(i, playerColor, playerFrameColor);
            activeHealthBars.Add(i, healthBar);
        }
    }

    public void UpdateHealthBar(int playerID, float currentHealth, float maxHealth)
    {
        if(activeHealthBars.TryGetValue(playerID, out HealthBar healthBar))
        {
            float fillAmount = currentHealth / maxHealth;
            healthBar.SetFillAmount();
        }
    }
}
