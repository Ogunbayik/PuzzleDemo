using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public event Action OnGameStart;
    public event Action<PlayerIdentity> OnEnemyTargetSelected;
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI _turnText;
    [SerializeField] private GameObject _targetButtonPanel;
    [SerializeField] private GameObject _targetSelectPanel;
    [SerializeField] private GameObject _targetButtonPrefab;
    [SerializeField] private Button _selectButton;

    private List<TargetSelectButton> _targetButtonList = new List<TargetSelectButton>();

    private TargetSelectButton _selectedTargetButton;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        HideTargetPanel();
        _selectButton.onClick.AddListener(() => SelectTargetPlayer());
    }

    private void SelectTargetPlayer()
    {
        if (_selectedTargetButton != null)
        {
            PlayerIdentity target = _selectedTargetButton.Target;
            TurnManager.Instance.SetTargetPlayer(target);
            OnEnemyTargetSelected?.Invoke(target);
        }
        else
            Debug.Log("First you need to choose a player");

    }
    public void SetupPanel(int targetCount)
    {
        DisplayTargetPanel();

        for (int i = 0; i < targetCount; i++)
        {
            var targetButton = Instantiate(_targetButtonPrefab, _targetButtonPanel.transform);
            _targetButtonList.Add(targetButton.GetComponent<TargetSelectButton>());

            var targets = TurnManager.Instance.GetTargetList();
            var targetIdentity = targets[i].GetComponent<PlayerIdentity>();

            targetButton.GetComponent<TargetSelectButton>().InitiliazeTargetButton(targetIdentity);
        }
    }
    public void SelectTargetButton(TargetSelectButton targetButton)
    {
        if (_selectedTargetButton != null)
            _selectedTargetButton.GetComponent<TargetSelectButton>().SetBackgroundColor(_selectedTargetButton.InitialColor);

        _selectedTargetButton = targetButton;
        _selectedTargetButton.GetComponent<TargetSelectButton>().SetBackgroundColor(_selectedTargetButton.SelectedColor);
    }
    public void DisplayTurnText()
    {
        _turnText.gameObject.SetActive(true);
    }
    public void HideTurnText()
    {
        _turnText.gameObject.SetActive(false);
    }
    public void ResetTargetButton()
    {
        foreach (var button in _targetButtonList)
            Destroy(button.gameObject);

        _targetButtonList.Clear();
    }
    public void DisplayTargetPanel()
    {
        _targetSelectPanel.SetActive(true);
    }
    public void HideTargetPanel()
    {
        _targetSelectPanel.SetActive(false);
    }
}
