using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private GameObject targetSelectPanel;
    [SerializeField] private GameObject targetPropertyUIPrefab;
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
            var targetList = GameManager.Instance.GetTargetList();
            var targetVisual = targetList[i].GetComponent<PlayerVisual>();
            var targetIdentity = targetList[i].GetComponent<PlayerIdentity>();
            var propertyUI = Instantiate(targetPropertyUIPrefab, targetSelectPanel.transform);
            propertyUI.GetComponent<TargetPropertyUI>().InitializeTargetProperty(targetVisual.PlayerSprite, targetIdentity.PlayerName);
        }
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
