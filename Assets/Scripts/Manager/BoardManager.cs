using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public event Action<PlayerIdentity> OnClickBomb;

    [Header("Game Settings")]
    [SerializeField] private int playerCount;
    [SerializeField] private int spriteCount;
    [SerializeField] private int bombCount;
    [Header("Board Settings")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float tileScale;
    [SerializeField] private GameObject tilePrefab;
    [Header("Color Settings")]
    [SerializeField] private Color selectedColor;
    [Header("UI Settings")]
    [SerializeField] private Sprite questionSprite;
    [SerializeField] private Sprite bombSprite;
    public List<Sprite> greenSprites = new List<Sprite>();
    public List<Sprite> redSprites = new List<Sprite>();
    public List<Sprite> yellowSprites = new List<Sprite>();
    public List<Sprite> blueSprites = new List<Sprite>();
    public List<Sprite> availableSprites = new List<Sprite>();

    private List<Sprite> copyGreenSprites;
    private List<Sprite> copyRedSprites;
    private List<Sprite> copyYellowSprites;
    private List<Sprite> copyBlueSprites;

    private Tile selectedTile;
    private Tile checkedTile;

    private Color tileColor;
    private int playerSpriteCount;
    private int selectCount;
    private int maxSelectCount = 2;
    private void Awake()
    {
        Instance = this;
        selectedTile = null;

        copyGreenSprites = new List<Sprite>(greenSprites);
        copyRedSprites = new List<Sprite>(redSprites);
        copyYellowSprites = new List<Sprite>(yellowSprites);
        copyBlueSprites = new List<Sprite>(blueSprites);

    }
    private void Start()
    {
        SetRandomAvailableList();
        SetupBoard();
    }
    private void SetRandomAvailableList()
    {
        //Demo for 25 Tile
        var boardTileCount = height * width;
        //Remain 24
        var remainTileCount = boardTileCount - bombCount;
        //PlayerSpriteCount = 6
        playerSpriteCount = remainTileCount / playerCount;
        for (int i = 0; i < spriteCount; i++)
        {
            var randomGreenIndex = UnityEngine.Random.Range(0, copyGreenSprites.Count);
            var randomRedIndex = UnityEngine.Random.Range(0, copyRedSprites.Count);
            var randomBlueIndex = UnityEngine.Random.Range(0, copyBlueSprites.Count);
            var randomYellowIndex = UnityEngine.Random.Range(0, copyYellowSprites.Count);

            //EachCount = 3
            var eachCount = playerSpriteCount / spriteCount;
            for (int j = 0; j < eachCount; j++)
            {
                availableSprites.Add(copyGreenSprites[randomGreenIndex]);
                availableSprites.Add(copyRedSprites[randomRedIndex]);
                availableSprites.Add(copyYellowSprites[randomYellowIndex]);
                availableSprites.Add(copyBlueSprites[randomBlueIndex]);
            }

            //Oyunu reset attýðýmýz zaman tüm spritelar silinmesin diye copy olarak aldýk.
            copyGreenSprites.RemoveAt(randomGreenIndex);
            copyRedSprites.RemoveAt(randomRedIndex);
            copyYellowSprites.RemoveAt(randomYellowIndex);
            copyBlueSprites.RemoveAt(randomBlueIndex);
        }

        availableSprites.Add(bombSprite);
    }
    private Sprite GetRandomAvailableSprite()
    {
        var randomIndex = UnityEngine.Random.Range(0, availableSprites.Count);
        var randomSprite = availableSprites[randomIndex];
        availableSprites.RemoveAt(randomIndex);
        return randomSprite;
    }
    private void SetupBoard()
    {
        selectCount = maxSelectCount;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var spawnPosition = Vector3.zero;
                var offsetY = 1f;
                spawnPosition.Set(i * tileScale, offsetY, j * tileScale);

                var tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                tile.transform.position = spawnPosition;
                tile.transform.SetParent(this.transform);
                tile.name = $"[{i},{j}]";

                var randomAvailableSprite = GetRandomAvailableSprite();

                if (greenSprites.Contains(randomAvailableSprite))
                    tileColor = GameManager.Instance.playerColors[Consts.GameSetup.GREEN_COLOR_INDEX];
                else if (blueSprites.Contains(randomAvailableSprite))
                    tileColor = GameManager.Instance.playerColors[Consts.GameSetup.BLUE_COLOR_INDEX];
                else if (redSprites.Contains(randomAvailableSprite))
                    tileColor = GameManager.Instance.playerColors[Consts.GameSetup.RED_COLOR_INDEX];
                else if (yellowSprites.Contains(randomAvailableSprite))
                    tileColor = GameManager.Instance.playerColors[Consts.GameSetup.YELLOW_COLOR_INDEX];

                tile.GetComponent<Tile>().SetupTile(i, j, randomAvailableSprite, questionSprite, tileColor);
            }
        }
    }
    public void SelectTile(Tile tile)
    {
        if (selectCount >= maxSelectCount)
            return;

        if(selectedTile == null)
        {
            //Select any tile on the board
            selectCount++;
            selectedTile = tile;
            selectedTile.GetComponent<Tile>().SetBackgroundColor(selectedColor);
            selectedTile.GetComponent<TileAnimationController>().PlayOpenTileAnimation();

            if(selectedTile.GetActualSprite() == bombSprite)
            {
                selectCount = maxSelectCount;
                CheckedForMatch(selectedTile, null);
            }

        }
        else if(selectedTile == tile)
        {
            selectedTile.GetComponent<TileAnimationController>().PlayWiggleTileAnimation();
            selectCount = 1;
            Debug.Log("Message: Please select new tile on the board");
        }
        else
        {
            //Check other tile is same or not
            if (checkedTile != null)
                return;

            checkedTile = tile;
            selectCount++;
            checkedTile.SetBackgroundColor(selectedColor);
            checkedTile.GetComponent<TileAnimationController>().PlayOpenTileAnimation();
            CheckedForMatch(selectedTile, checkedTile);
        }
    }
    private void CheckedForMatch(Tile selectedTile, Tile checkedTile)
    {
        if (checkedTile != null)
        {
            //First selected tile is not the bomb
            var selectedTileSprite = selectedTile.GetActualSprite();
            var checkTileSprite = checkedTile.GetActualSprite();
            if (checkTileSprite == bombSprite)
            {
                HandleClickBombSequence(selectedTile, checkedTile);
            }
            else if (selectedTileSprite == checkTileSprite)
            {
                HandleCorrectMatchSequence(selectedTile, checkedTile);
            }
            else
            {
                HandleMissMatchSequence(selectedTile, checkedTile);
            }
        }
        else
        {
            //First selected tile is the bomb
            HandleClickBombSequence(selectedTile, checkedTile);
        }
    }
    private void HandleClickBombSequence(Tile selectedTile, Tile checkedTile)
    {
        Sequence bombSequence = DOTween.Sequence();
        //First Part
        bombSequence.AppendInterval(Consts.TileAnimationTime.OPEN_ANIMATION_DURATION);
        bombSequence.AppendCallback(() => VFXManager.Instance.PlayExplosionVFX(checkedTile != null ? checkedTile.transform.position : selectedTile.transform.position));
        //Second Part
        var currentPlayer = TurnManager.Instance.GetCurrentPlayer();
        bombSequence.AppendInterval(Consts.DelayTime.EXPLOSION_VFX_DURATION);
        bombSequence.AppendCallback(() => OnClickBomb?.Invoke(currentPlayer));
        //Last Part
        bombSequence.AppendInterval(Consts.DelayTime.REFRESH_BOARD_DELAY);
        bombSequence.AppendCallback(() => RefreshBoard());
    }
    private void HandleCorrectMatchSequence(Tile selectedTile, Tile checkedTile)
    {
        Sequence correctSequence = DOTween.Sequence();
        //First Part
        correctSequence.AppendCallback(() => TurnManager.Instance.SetTargetList());
        //Second Part
        var currentPlayerVisual = TurnManager.Instance.GetCurrentPlayer().GetComponent<PlayerVisual>();
        correctSequence.AppendInterval(Consts.TileAnimationTime.OPEN_ANIMATION_DURATION);
        correctSequence.AppendCallback(() => selectedTile.SetBackgroundColor(currentPlayerVisual.PlayerColor));
        correctSequence.JoinCallback(() => checkedTile.SetBackgroundColor(currentPlayerVisual.PlayerColor));
        correctSequence.JoinCallback(() => CheckPlayerColor(currentPlayerVisual, checkedTile));
        //Third Part
        correctSequence.AppendInterval(Consts.TileAnimationTime.ANIMATION_DELAY_SHORT);
        correctSequence.AppendCallback(() => selectedTile.GetComponent<TileAnimationController>().PlayMatchTileAnimation());
        correctSequence.JoinCallback(() => checkedTile.GetComponent<TileAnimationController>().PlayMatchTileAnimation());
        //Last Part
        var targetCount = TurnManager.Instance.GetTargetList().Count;
        correctSequence.AppendInterval(Consts.TileAnimationTime.OPEN_PANEL_DELAY);
        correctSequence.AppendCallback(() => GameUIManager.Instance.SetupPanel(targetCount));
    }
    private void CheckPlayerColor(PlayerVisual player, Tile matchTile)
    {
        //Check currentPlayer damage with multiply or not..
        if (player.PlayerColor == matchTile.GetTileColor())
            TurnManager.Instance.SetDoubleDamageState(true);
        else
            TurnManager.Instance.SetDoubleDamageState(false);
    }
    private void HandleMissMatchSequence(Tile selectedTile, Tile checkedTile)
    {
        Sequence missSequence = DOTween.Sequence();
        //First Part
        missSequence.AppendInterval(Consts.TileAnimationTime.CLOSE_ANIMATION_DURATION);
        missSequence.AppendCallback(() => selectedTile.GetComponent<TileAnimationController>().PlayMissTileAnimation());
        missSequence.JoinCallback(() => checkedTile.GetComponent<TileAnimationController>().PlayMissTileAnimation());
        //Second Part
        missSequence.AppendInterval(Consts.TileAnimationTime.ANIMATION_DELAY_SHORT);
        missSequence.AppendCallback(() => selectedTile.SetBackgroundColor(Color.white));
        missSequence.JoinCallback(() => checkedTile.SetBackgroundColor(Color.white));
        //Last Part 
        missSequence.AppendInterval(Consts.DelayTime.ADVANCE_TURN_DELAY);
        missSequence.AppendCallback(() => TurnManager.Instance.AdvanceTurn());
        missSequence.JoinCallback(() => ResetSelectedTiles());
    }
    private void RefreshBoard()
    {
        List<GameObject> allTiles = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
            allTiles.Add(transform.GetChild(i).gameObject);

        foreach (var tile in allTiles)
            tile.GetComponent<TileAnimationController>().PlayRandomFallAnimation();
    }
    public void ResetSelectedTiles()
    {
        selectCount = 0;
        selectedTile = null;
        checkedTile = null;
    }
}
