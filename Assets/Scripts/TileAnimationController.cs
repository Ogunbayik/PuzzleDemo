using UnityEngine;

public class TileAnimationController : MonoBehaviour
{
    private Animator tileAnimator;
    private void Awake()
    {
        tileAnimator = GetComponent<Animator>();
    }

    public void SelectTileAnimation()
    {
        tileAnimator.SetTrigger("isSelect");
    }
    public void MatchTileAnimation()
    {
        tileAnimator.SetTrigger("isMatch");
    }
    public void MissTileAnimation()
    {
        tileAnimator.SetTrigger("isMiss");
    }
    public void WiggleTileAnimation()
    {
        tileAnimator.SetTrigger("isWiggle");
    }
}
