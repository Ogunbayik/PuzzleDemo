using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetPropertyUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image targetImage;
    [SerializeField] private TextMeshProUGUI targetText;

    public void InitializeTargetProperty(Sprite sprite, string playerName)
    {
        targetImage.sprite = sprite;
        targetText.text = "Player " + playerName;
    }
}
