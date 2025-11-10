using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private int maximumHealth;
    [SerializeField] private Image imageFill;
    [SerializeField] private Image imageFrame;

    private float currentHealth;

    private int index;
    public void SetupHealthBar(int playerIndex, Color fillColor, Color frameColor)
    {
        currentHealth = maximumHealth;
        index = playerIndex;
        imageFill.color = fillColor;
        imageFrame.color = frameColor;
        imageFill.fillAmount = (float)currentHealth;
    }

    public void SetFillAmount()
    {
        imageFill.fillAmount = (float)currentHealth;
    }
}
