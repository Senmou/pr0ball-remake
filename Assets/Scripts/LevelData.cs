public static class LevelData {

    public const int wavesPerLevel = 20;

    private static int wave = 1;
    private static int level = 1;

    public static int Level { get => level; }

    public static bool IsBossLevel { get => level % 10 == 0; }

    public static void SetCurrentLevelData(CurrentLevelData currentLevelData) {
        wave = currentLevelData.wave;
        level = currentLevelData.level;
    }

    public static void LevelUp() {
        level++;
        wave = 1;
    }

    public static void BossLevelFailed() {
        level -= 2;
        wave = 1;
    }

    public static int Wave {
        get => wave;
        set {
            wave = value;
            if (wave > wavesPerLevel) {
                wave = 1;
                level++;
            }
        }
    }
}
