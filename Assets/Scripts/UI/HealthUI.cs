using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image imageFill;
    [SerializeField] private Image remainFill;
    [SerializeField] private Image imageFrame;

    private void Awake()
    {
        imageFill.fillAmount = 1;
        remainFill.fillAmount = 1;
    }
    public void InitializeBar(Vector3 offsetY, Color fillColor, Color frameColor)
    {
        imageFill.color = fillColor;
        imageFrame.color = frameColor;
        remainFill.color = frameColor;
        transform.position = transform.position + offsetY;

        var healthUIRotation = new Vector3(45f, -45f, 0f);
        transform.rotation = Quaternion.Euler(healthUIRotation);
    }
    public void HandleHealthChange(int currentHealth, int maxHealth, float delayTime)
    {
        var amount = (float)currentHealth / maxHealth;
        imageFill.fillAmount = amount;

        StartCoroutine(DelayedRemainFillDecrease(delayTime));
    }
    public IEnumerator DelayedRemainFillDecrease(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        while(remainFill.fillAmount > imageFill.fillAmount)
        {
            remainFill.fillAmount = Mathf.MoveTowards(remainFill.fillAmount, imageFill.fillAmount, Consts.GameSetup.HEALTH_FILL_LERP_SPEED * Time.deltaTime);

            yield return null;
        }

        remainFill.fillAmount = imageFill.fillAmount;
    }
}
