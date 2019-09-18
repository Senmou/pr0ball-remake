public static class LevelData {

    public static int wave = 1;
    public static int level = 1;
    public static bool UpcomingBossLevel {
        get => level % 10 == 9;
    }

    public const int wavesPerLevel = 20;


}
