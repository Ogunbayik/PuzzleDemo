using System.Collections;
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
        StartCoroutine(nameof(SpawnPlayerSequence));
    }
    private void SetSpawnPosition()
    {
        for (int i = 0; i < playerCount; i++)
            spawnPositionList.Add(spawnPosition[i].transform.position);
    }
    private IEnumerator SpawnPlayerSequence()
    {
        for (int i = 0; i < playerCount; i++)
        {
            CameraManager.Instance.SetHelperCameraPosition(spawnPositionList[i]);
            CameraManager.Instance.ToggleGameCamera();
            VFXManager.Instance.PlaySpawnVFX(spawnPosition[i].transform.position);

            yield return new WaitForSeconds(Consts.DelayTime.SPAWN_PLAYER_DELAY);

            var player = Instantiate(playerPrefab);
            var playerIdentity = player.GetComponent<PlayerIdentity>();
            var playerVisual = player.GetComponent<PlayerVisual>();
            var playerAttack = player.GetComponent<PlayerAttack>();
            var playerHealth = player.GetComponent<PlayerHealth>();
            player.transform.position = spawnPositionList[i];

            playerIdentity.InitializePlayerID(i, playerNames[i]);
            playerVisual.InitializeVisual(playerColors[i], frameColors[i], playerSprites[i]);
            playerAttack.InitializeAttackPosition();

            var offsetUpY = new Vector3(0f, 4f, 0f);
            playerHealth.InitializeHealthBar(offsetUpY);

            TurnManager.Instance.AddPlayer(playerIdentity);
            yield return new WaitForSeconds(Consts.DelayTime.CHANGE_CAMERA_DELAY);
            CameraManager.Instance.ToggleGameCamera();
        }

        yield return new WaitForSeconds(Consts.DelayTime.START_GAME_DELAY);

        TurnManager.Instance.StartGame();
        Debug.Log("Game is starting");
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
