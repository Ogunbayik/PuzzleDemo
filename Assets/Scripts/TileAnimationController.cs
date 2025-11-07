using UnityEngine;

public class TileAnimationController : MonoBehaviour
{
    private Animator tileAnimator;
    private void Awake()
    {
        tileAnimator = GetComponent<Animator>();
    }

    public void PlaySelectTileAnimation()
    {
        tileAnimator.SetTrigger(Consts.TileAnimationParameter.IS_SELECT);
    }
    public void PlayMatchTileAnimation()
    {
        tileAnimator.SetTrigger(Consts.TileAnimationParameter.IS_MATCH);
    }
    public void PlayMissTileAnimation()
    {
        tileAnimator.SetTrigger(Consts.TileAnimationParameter.IS_MISS);
    }
    public void PlayWiggleTileAnimation()
    {
        tileAnimator.SetTrigger(Consts.TileAnimationParameter.IS_WIGGLE);
    }
    
    public void PlayRandomFallAnimation()
    {
        var totalFallAnimation = 4;
        var randomIndex = Random.Range(0, totalFallAnimation);
    }
}
