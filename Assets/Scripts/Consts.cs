public static class Consts 
{
    public static class TileAnimationTime
    {
        public const float MINIMUM_FALL_SPEED = 0.5f;
        public const float MAXIMUM_FALL_SPEED = 1.5f;
        public const float OPEN_ANIMATION_DURATION = 1f;
        public const float CLOSE_ANIMATION_DURATION = 1f;
        public const float ANIMATION_DELAY_SHORT = 0.5f;
        public const float OPEN_PANEL_DELAY = 1.5f;
    }
    public static class PlayerAnimationTime
    {
        public const float ATTACK_ANIMATION_DURATION = 2.2f;
        public const float HIT_ANIMATION_DURATION = 1.2f;
        public const float DEAD_ANIMATION_DURATION = 2.3f;
    }
    public static class GameSetup
    {
        public const float HEALTH_FILL_LERP_SPEED = 2f;
        public const int PLAYER_COUNT_SPECIAL_SETUP = 3;
        public const int GREEN_COLOR_INDEX = 0;
        public const int BLUE_COLOR_INDEX = 1;
        public const int RED_COLOR_INDEX = 2;
        public const int YELLOW_COLOR_INDEX = 3;
    }
    public static class PlayerMaterial
    {
        public const string BODY_MAIN = "Body";
        public const string BODY_STRIPE = "Body_2";
    }
    public static class GameDamage
    {
        public const int BOMB_DAMAGE = 25;
        public const int FIREBALL_DAMAGE = 10;
        public const int DOUBLE_DAMAGE_MULTIPLIER = 2;
        public const int DEFAULT_MULTIPLIER = 1;
    }
    public static class DelayTime
    {
        //GameManager Spawn
        public const float SPAWN_PLAYER_DELAY = 3f;
        public const float CHANGE_CAMERA_DELAY = 1f;
        public const float START_GAME_DELAY = 2f;
        //PlayerHealth Hit and Dead
        public const float PLAYER_HEALTH_CHANGE_DELAY = 1f;
        public const float REMAINFILL_DECREASE_DELAY = 2f;
        public const float START_PLAYER_DEAD_DELAY = 1f;
        public const float ADVANCE_TURN_DELAY = 2f;
        //BoardManager ClickBomb
        public const float EXPLOSION_VFX_DURATION = 4f;
        public const float REFRESH_BOARD_DELAY = 1f;
        public const float ACTIVATE_SHIELD_DELAY = 1f;
        public const float PLAYER_LOOK_TIME = 1f;
    }

    public static class CameraPriority
    {
        public const int ACTIVE_PRIORITY = 10;
        public const int INACTIVE_PRIORITY = 1;
    }

}

