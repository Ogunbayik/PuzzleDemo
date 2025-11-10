using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private int playerCount;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPosition;

    public List<Vector3> spawnPositionList = new List<Vector3>();
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
        GameSetup();
    }
    private void GameSetup()
    {
        SetSpawnPosition();
    }

    private void SetSpawnPosition()
    {
        if (playerCount == Consts.GameSetup.PLAYER_COUNT_SPECIAL_SETUP)
        {
            var lastSpawnPositionIndex = 3;
            for (int i = 0; i < lastSpawnPositionIndex - 1; i++)
                spawnPositionList.Add(spawnPosition[i].transform.position);

            //Add last two transform mid position
            Vector3 mergedSpawnPoint = (spawnPosition[lastSpawnPositionIndex].transform.position + spawnPosition[lastSpawnPositionIndex - 1].transform.position) / 2f;
            spawnPositionList.Add(mergedSpawnPoint);
        }
        else
        {
            for (int i = 0; i < playerCount; i++)
            {
                spawnPositionList.Add(spawnPosition[i].transform.position);
            }
        }

        for (int i = 0; i < playerCount; i++)
        {
            var player = Instantiate(playerPrefab);
            player.transform.position = spawnPositionList[i];
        }
    }
}
