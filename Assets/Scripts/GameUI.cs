using System;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private GameObject targetImagePanel;
    [SerializeField] private GameObject targetSelectPanel;
    [SerializeField] private GameObject targetPropertyUIPrefab;

    private List<TargetPropertyUI> propertyUIList = new List<TargetPropertyUI>();

    private TargetPropertyUI selectedPropertyUI;
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

        SelectPanelDeactivate();
    }

    public void SetupPanel(int targetCount)
    {
        SelectPanelActivate();

        for (int i = 0; i < targetCount; i++)
        {
            var propertyUI = Instantiate(targetPropertyUIPrefab, targetImagePanel.transform);
            propertyUIList.Add(propertyUI.GetComponent<TargetPropertyUI>());

            var targets = TurnManager.Instance.GetTargetList();
            Debug.Log(targets.Count);
            var targetVisual = targets[i].GetComponent<PlayerVisual>();
            var targetIdentity = targets[i].GetComponent<PlayerIdentity>();

            var targetFrameColor = targetVisual.FrameColor;
            targetFrameColor.a = 0.4f;
            propertyUI.GetComponent<TargetPropertyUI>().InitializeTargetProperty(targetIdentity,targetVisual.PlayerSprite, targetIdentity.PlayerName,targetVisual.PlayerColor, targetFrameColor);
        }
    }
    public void SetSelectedPropertyUI(TargetPropertyUI property)
    {
        if (selectedPropertyUI != null)
            selectedPropertyUI.GetComponent<TargetPropertyUI>().SetBackgroundColor(selectedPropertyUI.InitialColor);

        selectedPropertyUI = property;
        selectedPropertyUI.GetComponent<TargetPropertyUI>().SetBackgroundColor(selectedPropertyUI.SelectedColor);

        TurnManager.Instance.SetTargetPlayer(selectedPropertyUI.Target);
    }
    public void ResetPropertyList()
    {
        foreach (var property in propertyUIList)
            Destroy(property.gameObject);

        propertyUIList.Clear();
    }
    public void SelectPanelActivate()
    {
        targetSelectPanel.SetActive(true);
    }
    public void SelectPanelDeactivate()
    {
        targetSelectPanel.SetActive(false);
    }
}
