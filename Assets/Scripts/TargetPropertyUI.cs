using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetPropertyUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image targetImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private Color selectedColor;

    private PlayerIdentity target;
    private Color initialColor;

    public PlayerIdentity Target => target;
    public Color InitialColor => initialColor;
    public Color SelectedColor => selectedColor;

    public void InitializeTargetProperty(PlayerIdentity targetIdentity,Sprite sprite, string playerName,Color textColor, Color backgroundColor)
    {
        target = targetIdentity;
        gameObject.name = playerName + "PropertyUI";
        targetImage.sprite = sprite;
        targetText.text = "Player " + playerName;
        targetText.color = textColor;
        backgroundImage.color = backgroundColor;
        initialColor = backgroundColor;
    }
    public void ClickProperty()
    {
        GameUI.Instance.SetSelectedPropertyUI(this);
    }
    public void SetBackgroundColor(Color color)
    {
        backgroundImage.color = color;
    }

}
