using UnityEngine;

public class TileAnimationController : MonoBehaviour
{
    private Animator tileAnimator;
    private void Awake()
    {
        tileAnimator = GetComponent<Animator>();
    }

    public void PlayOpenTileAnimation()
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
        var totalFallAnimation = 2;
        var randomIndex = Random.Range(0, totalFallAnimation);
        var randomAnimationSpeed = Random.Range(Consts.TileAnimationTime.MINIMUM_FALL_SPEED, Consts.TileAnimationTime.MAXIMUM_FALL_SPEED);
        
        tileAnimator.speed = randomAnimationSpeed;

        if (randomIndex == 0)
            tileAnimator.SetTrigger(Consts.TileAnimationParameter.IS_FALLLEFT);
        else
            tileAnimator.SetTrigger(Consts.TileAnimationParameter.IS_FALLRIGHT);

    }
}
