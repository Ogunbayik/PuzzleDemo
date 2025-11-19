using UnityEngine;

public static class TileAnimationHashes
{
    public static readonly int IS_SELECT_HASH = Animator.StringToHash("isSelect");
    public static readonly int IS_MATCH_HASH = Animator.StringToHash("isMatch");
    public static readonly int IS_MISS_HASH = Animator.StringToHash("isMiss");
    public static readonly int IS_WIGGLE_HASH = Animator.StringToHash("isWiggle");
    public static readonly int IS_FALLLEFT_HASH = Animator.StringToHash("isFallLeft");
    public static readonly int IS_FALLRIGHT_HASH = Animator.StringToHash("isFallRight");
}
