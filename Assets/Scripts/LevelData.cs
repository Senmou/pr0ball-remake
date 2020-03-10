using UnityEngine;

public static class LevelData {

    public const int wavesPerLevel = 20;

    private static int wave = 1;
    private static int level = 1;
    private static int dangerLevel = 0;

    public static int Level {
        get => level;
        set {
            level = value;
            GameObject.FindObjectOfType<LevelUI>()?.UpdateLevelUI();
        }
    }

    public static int DangerLevel {
        get => dangerLevel;
        set {
            dangerLevel = value;
            if (dangerLevel < 0)
                dangerLevel = 0;

            GameObject.FindObjectOfType<LevelUI>()?.UpdateDangerLevelUI();
        }
    }

    public static void SetCurrentLevelData(CurrentLevelData currentLevelData) {
        wave = currentLevelData.wave;
        Level = currentLevelData.level;
        DangerLevel = currentLevelData.dangerLevel;

        GameObject.FindObjectOfType<LevelUI>()?.UpdateLevelUI();
    }

    public static void LevelUp() {
        Level++;
        DangerLevel += Level;
        wave = 1;
        EventManager.TriggerEvent("ReachedNextLevel");
    }

    public static int Wave {
        get => wave;
        set {
            wave = value;
            if (wave > wavesPerLevel) {
                wave = 1;
                Level++;
                EventManager.TriggerEvent("ReachedNextLevel");
            }
        }
    }

    public static void ResetData() {
        wave = 1;
        Level = 1;
        DangerLevel = 0;
    }
}
