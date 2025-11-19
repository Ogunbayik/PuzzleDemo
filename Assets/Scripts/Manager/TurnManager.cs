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
            playerAnimationController.PlayActiveIdleAnimation();
        }
    }
    public void AdvanceTurn()
    {
        currentPlayer.GetComponentInChildren<PlayerAnimationController>().PlayPassiveIdleAnimation();

        if (playerIdentities.Count == currentPlayerIndex + 1)
            currentPlayerIndex = 0;
        else
            currentPlayerIndex++;

        currentPlayer = playerIdentities[currentPlayerIndex];
        targetPlayer = null;
        targetIdentities.Clear();
        currentPlayer.GetComponentInChildren<PlayerAnimationController>().PlayActiveIdleAnimation();
    }
    public void SetTargetList()
    {
        targetIdentities.AddRange(playerIdentities);
        targetIdentities.Remove(currentPlayer);
    }
    public void RemoveDeadPlayer(PlayerIdentity player)
    {
        playerIdentities.Remove(player);
    }
    public void SetTargetPlayer(PlayerIdentity target)
    {
        targetPlayer = target;
    }
    public List<PlayerIdentity> GetTargetList()
    {
        return playerIdentities.Where((currentPlayer, i) => i != currentPlayerIndex).ToList();
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
