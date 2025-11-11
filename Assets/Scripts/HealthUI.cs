using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image imageFill;
    [SerializeField] private Image imageFrame;

    public void InitializeBar(Vector3 offsetY, Color fillColor, Color frameColor)
    {
        imageFill.color = fillColor;
        imageFrame.color = frameColor;
        transform.position = transform.position + offsetY;
    }
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float amount = (float)currentHealth / maxHealth;
        imageFill.fillAmount = amount;
    }
}
