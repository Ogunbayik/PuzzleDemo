using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int playerCount;
    public Transform[] spawnPosition;
    [Header("Player Settings")]
    public GameObject playerPrefab;
    public Color[] playerColors;
    public Color[] frameColors;
    public string[] playerNames;
    public Sprite[] playerSprites;

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
        HandleSpawnPlayerSequence();
    }
    private void HandleSpawnPlayerSequence()
    {
        Sequence spawnSequence = DOTween.Sequence();

        for (int i = 0; i < playerCount; i++)
        {
            int playerIndex = i;
            //First Part
            spawnSequence.AppendCallback(() => CameraManager.Instance.SetHelperCameraPosition(spawnPosition[playerIndex].transform.position));
            spawnSequence.JoinCallback(() => CameraManager.Instance.ToggleGameCamera());
            spawnSequence.JoinCallback(() => VFXManager.Instance.PlaySpawnVFX(spawnPosition[playerIndex].transform.position));
            //Second Part
            spawnSequence.AppendInterval(Consts.DelayTime.SPAWN_PLAYER_DELAY);
            spawnSequence.AppendCallback(() => CreatePlayer(playerIndex));
            //Third Part
            spawnSequence.AppendInterval(Consts.DelayTime.CHANGE_CAMERA_DELAY);
            spawnSequence.AppendCallback(() => CameraManager.Instance.ToggleGameCamera());
        }
        //Last Part
        spawnSequence.AppendInterval(Consts.DelayTime.START_GAME_DELAY);
        spawnSequence.AppendCallback(() => TurnManager.Instance.StartGame());
    }

    private void CreatePlayer(int playerIndex)
    {
        var player = Instantiate(playerPrefab);
        var playerIdentity = player.GetComponent<PlayerIdentity>();
        var playerVisual = player.GetComponent<PlayerVisual>();
        var playerHealth = player.GetComponent<PlayerHealth>();
        player.transform.position = spawnPosition[playerIndex].transform.position;

        playerIdentity.InitializePlayerID(playerIndex, playerNames[playerIndex]);
        playerVisual.InitializeVisual(playerColors[playerIndex], frameColors[playerIndex], playerSprites[playerIndex]);

        var offsetUpY = new Vector3(0f, 4f, 0f);
        playerHealth.InitializeHealthBar(offsetUpY);

        TurnManager.Instance.AddPlayer(playerIdentity);
    }

}
