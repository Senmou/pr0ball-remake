using System;

[System.Serializable]
public class BallStats {

    #region Singleton
    private static BallStats instance;
    public static BallStats Instance {
        get {
            if (instance == null)
                instance = new BallStats();
            return instance;
        }
    }
    #endregion

    // Stats
    public int level;

    public int damage;
    public float critChance;
    public float critDamage;
    public int ballCount;

    public int UpgradeDamage { get => VPS(level, (1, 1), (10, 9)); }
    public float UpgradeCritChance { get => 5f; }
    public float UpgradeCritDamage { get => 0.5f; }
    public int UpgradeBallCount { get => VES(level, (2, 1)); }

    public int UpgradePrice { get => 1 + VPS(level, (5, 1)); }

    public void AddStats() {
        damage += UpgradeDamage;
        critChance += UpgradeCritChance;
        critDamage += UpgradeCritDamage;
        ballCount += UpgradeBallCount;
        level++;
    }

    public void ResetStats() {
        level = 1;
        damage = 1;
        critChance = 0f;
        critDamage = 2f;
        ballCount = 1;
    }

    public int ModifiedDamage() {

        float dmg = damage;

        float r = UnityEngine.Random.Range(0f, 100f);
        if (r < critChance)
            dmg *= critDamage;

        return (int)dmg;
    }

    // Helper function (VPS = ValuePerStep)
    private int VPS(int level, params ValueTuple<int, int>[] stepAndValue) {
        int sum = 0;
        foreach (var pair in stepAndValue) {
            int step = pair.Item1;
            int value = pair.Item2;
            sum += (level / step) * value;
        }
        return sum;
    }

    private float VPS(int level, params ValueTuple<int, float>[] stepAndValue) {
        float sum = 0f;
        foreach (var pair in stepAndValue) {
            int step = pair.Item1;
            float value = pair.Item2;
            sum += (level / step) * value;
        }
        return sum;
    }

    // Helper function (VES = ValueEverySteps)
    private int VES(int level, params ValueTuple<int, int>[] stepAndValue) {
        int sum = 0;
        foreach (var pair in stepAndValue) {
            int step = pair.Item1;
            int value = pair.Item2;
            if (level % step == 0)
                sum += value;
        }
        return sum;
    }
}
