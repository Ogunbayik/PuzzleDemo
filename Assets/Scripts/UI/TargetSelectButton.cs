using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelectButton : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image _targetImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _targetText;
    [SerializeField] private Color _selectedColor;


    private PlayerIdentity _target;
    private Color _initialColor;
    private Button selectButton;

    public PlayerIdentity Target => _target;
    public Color InitialColor => _initialColor;
    public Color SelectedColor => _selectedColor;
    private void Awake()
    {
        selectButton = _targetImage.GetComponent<Button>();

        if (selectButton != null)
            selectButton.onClick.AddListener(() => GameUIManager.Instance.SelectTargetButton(this));
    }

    public void InitiliazeTargetButton(PlayerIdentity targetIdentity)
    {
        _target = targetIdentity;
        gameObject.name = targetIdentity.PlayerName + "Button";
        _targetText.text = "Player " + targetIdentity.PlayerName;

        _targetImage.sprite = targetIdentity.GetComponent<PlayerVisual>().PlayerSprite;
        _targetText.color = targetIdentity.GetComponent<PlayerVisual>().PlayerColor;
        _backgroundImage.color = targetIdentity.GetComponent<PlayerVisual>().FrameColor;
        _initialColor = _backgroundImage.color;
    }
    public void SetBackgroundColor(Color color)
    {
        _backgroundImage.color = color;
    }

}
