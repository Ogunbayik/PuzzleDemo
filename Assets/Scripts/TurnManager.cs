using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public List<PlayerIdentity> playerIdentities = new List<PlayerIdentity>();
    public List<PlayerIdentity> targetIdentities = new List<PlayerIdentity>();

    private PlayerIdentity currentPlayer;
    private PlayerIdentity targetPlayer;
    private int currentPlayerIndex;
    private int minimumPlayerCount = 2;
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
    public void AddPlayer(PlayerIdentity player)
    {
        playerIdentities.Add(player);
    }

    public void StartGame()
    {
        if(playerIdentities.Count >= minimumPlayerCount)
        {
            currentPlayerIndex = 0;
            currentPlayer = playerIdentities[currentPlayerIndex];

            var playerAnimationController = currentPlayer.GetComponentInChildren<PlayerAnimationController>();
            playerAnimationController.PlayTurnIdleAnimation();
        }
    }
    public void AdvanceTurn()
    {
        var nextPlayer = playerIdentities[currentPlayerIndex + 1];
        var isNextPlayerDead = nextPlayer.GetComponent<PlayerHealth>().IsDead();

        if (!isNextPlayerDead)
        {
            currentPlayerIndex++;
            currentPlayer = playerIdentities[currentPlayerIndex];
        }
        else
        {
            if (playerIdentities.Count > 2)
                currentPlayerIndex = currentPlayerIndex + 2;
            else
                currentPlayerIndex = 0;
        }
    }
    public void SetTargetList()
    {
        targetIdentities.Clear();
        targetIdentities.AddRange(playerIdentities);
        targetIdentities.Remove(currentPlayer);
    }
    public List<PlayerIdentity> GetTargetList()
    {
        return playerIdentities.Where((currentPlayer, i) => i != currentPlayerIndex).ToList();
    }
    public void SetTargetPlayer(PlayerIdentity target)
    {
        targetPlayer = target;
    }
    public PlayerIdentity GetCurrentPlayer()
    {
        return currentPlayer;
    }
    public PlayerIdentity GetTargetPlayer()
    {
        return targetPlayer;
    }
}
