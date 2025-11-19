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
        tileAnimator.SetTrigger(TileAnimationHashes.IS_SELECT_HASH);
    }
    public void PlayMatchTileAnimation()
    {
        tileAnimator.SetTrigger(TileAnimationHashes.IS_MATCH_HASH);
    }
    public void PlayMissTileAnimation()
    {
        tileAnimator.SetTrigger(TileAnimationHashes.IS_MISS_HASH);
    }
    public void PlayWiggleTileAnimation()
    {
        tileAnimator.SetTrigger(TileAnimationHashes.IS_WIGGLE_HASH);
    }
    public void PlayRandomFallAnimation()
    {
        var totalFallAnimation = 2;
        var randomIndex = Random.Range(0, totalFallAnimation);
        var randomAnimationSpeed = Random.Range(Consts.TileAnimationTime.MINIMUM_FALL_SPEED, Consts.TileAnimationTime.MAXIMUM_FALL_SPEED);
        
        tileAnimator.speed = randomAnimationSpeed;

        if (randomIndex == 0)
            tileAnimator.SetTrigger(TileAnimationHashes.IS_FALLLEFT_HASH);
        else
            tileAnimator.SetTrigger(TileAnimationHashes.IS_FALLRIGHT_HASH);

    }
}
