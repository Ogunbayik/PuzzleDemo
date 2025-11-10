public class Consts 
{
    public struct TileAnimationParameter
    {
        public const string IS_SELECT = "isSelect";
        public const string IS_MATCH = "isMatch";
        public const string IS_MISS = "isMiss";
        public const string IS_WIGGLE = "isWiggle";
        public const string IS_FALLLEFT = "isFallLeft";
        public const string IS_FALLRIGHT = "isFallRight";
    }
    public struct TileAnimationTime
    {
        public const float MINIMUM_FALL_SPEED = 0.5f;
        public const float MAXIMUM_FALL_SPEED = 1.5f;
        public const float OPEN_ANIMATION_TIME = 1.1f;
        public const float ANIMATION_DELAY_SHORT = 0.5f;
    }
    public struct GameSetup
    {
        public const int PLAYER_COUNT_SPECIAL_SETUP = 3;
    }


}

