using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerIdentity targetPlayer;

    private List<PlayerIdentity> allPlayersList = new List<PlayerIdentity>();
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
            var playerIdentity = player.GetComponent<PlayerIdentity>();
            var playerVisual = player.GetComponent<PlayerVisual>();
            var playerAttack = player.GetComponent<PlayerAttack>();
            var playerHealth = player.GetComponent<PlayerHealth>();
            player.transform.position = spawnPositionList[i];

            playerIdentity.InitializePlayerID(i, playerNames[i]);
            playerVisual.InitializeVisual(playerColors[i], frameColors[i], playerSprites[i]);
            playerAttack.InitializeAttackPosition();

            var offsetUpY = new Vector3(0f, 3.5f, 0f);
            var offsetDownY = new Vector3(0f, -1.2f, 0f);

            if (i < Consts.GameSetup.PLAYER_COUNT_SPECIAL_SETUP - 1)
                playerHealth.InitializeHealthBar(offsetUpY);
            else
                playerHealth.InitializeHealthBar(offsetDownY);

            TurnManager.Instance.AddPlayer(playerIdentity);
        }

        TurnManager.Instance.StartGame();
    }
    public void SetTargetPlayer(PlayerIdentity targetIdentity)
    {
        targetPlayer = targetIdentity;
    }
    public void ExecuteAttack()
    {
        //When I click select button in TargetPanel, Execute attacking
        var currentPlayer = TurnManager.Instance.GetCurrentPlayer();
        currentPlayer.GetComponent<PlayerAttack>().StartAttackSequence();
        GameUIManager.Instance.HideTargetPanel();
        GameUIManager.Instance.ResetPropertyList();
    }
    public void ChangePlayerTurn()
    {
        if (currentPlayerIndex >= allPlayersList.Count - 1)
            currentPlayerIndex = 0;
        else
            currentPlayerIndex++;
    }
    public void RemoveDeadPlayerOnList(PlayerIdentity deadPlayer)
    {
        if (allPlayersList.Contains(deadPlayer))
            allPlayersList.Remove(deadPlayer);
    }
}
