using UnityEngine;

public class PlayerIdentity : MonoBehaviour
{
    private int playerID;
    private string playerName;
    public int PlayerID => playerID;
    public string PlayerName => playerName;
    public void InitializePlayerID(int id, string name)
    {
        playerID = id;
        playerName = name;
    }
}
