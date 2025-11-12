using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerIdentity targetPlayer;

    private List<PlayerIdentity> allPlayers = new List<PlayerIdentity>();
    [Header("Game Settings")]
    public int playerCount;
    public GameObject playerPrefab;
    public Transform[] spawnPosition;
    [Header("Color Settings")]
    public Color[] playerColors;
    public Color[] frameColors;
    public string[] playerNames;
    public Sprite[] playerSprites;

    public List<Vector3> spawnPositionList = new List<Vector3>();

    public PlayerIdentity TargetPlayer => targetPlayer;

    private int currentPlayerIndex = 0;

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
    }

    void Start()
    {
        SetSpawnPosition();
        SetupPlayer();
    }
    private void SetSpawnPosition()
    {
        if (playerCount == Consts.GameSetup.PLAYER_COUNT_SPECIAL_SETUP)
        {
            var lastSpawnPositionIndex = spawnPosition.Length - 1;
            for (int i = 0; i < lastSpawnPositionIndex - 1; i++)
                spawnPositionList.Add(spawnPosition[i].transform.position);

            //Add last two transform mid position
            Vector3 mergedSpawnPoint = (spawnPosition[lastSpawnPositionIndex].transform.position + spawnPosition[lastSpawnPositionIndex - 1].transform.position) / 2f;
            spawnPositionList.Add(mergedSpawnPoint);
        }
        else
        {
            for (int i = 0; i < playerCount; i++)
                spawnPositionList.Add(spawnPosition[i].transform.position);
        }
    }
    private void SetupPlayer()
    {
        for (int i = 0; i < playerCount; i++)
        {
            var player = Instantiate(playerPrefab);
            player.transform.position = spawnPositionList[i];

            var playerID = i + 1;
            player.GetComponent<PlayerIdentity>().InitializePlayerID(playerID, playerNames[i]);
            player.GetComponent<PlayerVisual>().InitializeVisual(playerColors[i], frameColors[i], playerSprites[i]);
            player.GetComponent<PlayerAttack>().InitializeAttackPosition();

            var offsetUpY = new Vector3(0f, 3.5f, 0f);
            var offsetDownY = new Vector3(0f, -1.2f, 0f);

            if (i < Consts.GameSetup.PLAYER_COUNT_SPECIAL_SETUP - 1)
                player.GetComponent<PlayerHealth>().InitializeHealthBar(offsetUpY);
            else
                player.GetComponent<PlayerHealth>().InitializeHealthBar(offsetDownY);

            allPlayers.Add(player.GetComponent<PlayerIdentity>());
        }
    }
    public void SetTargetPlayer(PlayerIdentity targetIdentity)
    {
        targetPlayer = targetIdentity;
    }
    public void ExecuteAttack()
    {
        var currentPlayer = allPlayers[currentPlayerIndex];
        currentPlayer.GetComponent<PlayerAttack>().StartAttacking();
        GameUI.Instance.SelectPanelDeactivate();
        GameUI.Instance.ResetPropertyList();
    }
    public void ChangePlayerTurn()
    {
        currentPlayerIndex++;
    }
    public List<PlayerIdentity> GetTargetList()
    {
        List<PlayerIdentity> targetList = new List<PlayerIdentity>();

        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (i != currentPlayerIndex)
                targetList.Add(allPlayers[i]);
        }

        return targetList;
    }
    public PlayerIdentity GetCurrentPlayer()
    {
        return allPlayers[currentPlayerIndex];
    }
}
