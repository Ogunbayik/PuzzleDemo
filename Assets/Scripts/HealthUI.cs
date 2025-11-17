using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private PlayerTrigger playerTrigger;

    private const float HEALTH_FILL_LERP_SPEED = 3f;

    [Header("UI Settings")]
    [SerializeField] private Image imageFill;
    [SerializeField] private Image remainFill;
    [SerializeField] private Image imageFrame;
    private void Awake()
    {
        playerTrigger = GetComponent<PlayerTrigger>();

        imageFill.fillAmount = 1;
        remainFill.fillAmount = 1;
    }
    public void InitializeBar(Vector3 offsetY, Color fillColor, Color frameColor)
    {
        imageFill.color = fillColor;
        imageFrame.color = frameColor;
        remainFill.color = frameColor;
        transform.position = transform.position + offsetY;
    }
    private void Update()
    {
        if (ShouldRemainFillDecrease())
            UpdateRemainFillAmount();
    }
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float amount = (float)currentHealth / maxHealth;
        imageFill.fillAmount = amount;
    }
    public void UpdateRemainFillAmount()
    {
        if (imageFill.fillAmount < remainFill.fillAmount)
            remainFill.fillAmount -= Time.deltaTime / HEALTH_FILL_LERP_SPEED;
    }
    public bool ShouldRemainFillDecrease()
    {
        if (remainFill.fillAmount >= imageFill.fillAmount)
            return true;
        else
            return false;
    }

}
