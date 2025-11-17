using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private Sprite playerSprite;
    private Color playerColor;
    private Color frameColor;

    public Color PlayerColor => playerColor;
    public Color FrameColor => frameColor;
    public Sprite PlayerSprite => playerSprite;
    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    public void InitializeVisual(Color playerCol, Color frameCol, Sprite sprite)
    {
        playerColor = playerCol;
        frameColor = frameCol;
        playerSprite = sprite;

        foreach (var material in meshRenderer.materials)
        {
            string cleanMatName = material.name.Replace(" (Instance)", "");
            Debug.Log(cleanMatName);

            if (cleanMatName == Consts.PlayerMaterial.BODY_MAIN)
                material.color = playerColor;
            else if (cleanMatName == Consts.PlayerMaterial.BODY_STRIPE)
                material.color = frameColor;
        }
    }
}
