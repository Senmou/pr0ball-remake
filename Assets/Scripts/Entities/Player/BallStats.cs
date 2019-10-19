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

    public int UpgradeDamage { get => VPS(level, (1, 1), (5, 5), (10, 100)); }
    public float UpgradeCritChance { get => 0.25f; }
    public float UpgradeCritDamage { get => 0.05f; }
    public int UpgradeBallCount { get => VES(level, (5, ballCount)); }

    public int UpgradePrice { get => level; }

    // Extra stats
    public int extraDamageLevel;
    public int extraCritChanceLevel;
    public int extraCritDamageLevel;
    public int extraBallCountLevel;

    public int extraDamage;
    public float extraCritChance;
    public float extraCritDamage;
    public int extraBallCount;

    public int UpgradeExtraDamage { get => extraDamageLevel; }
    public float UpgradeExtraCritChance { get => extraCritChanceLevel; }
    public float UpgradeExtraCritDamage { get => extraCritDamageLevel; }
    public int UpgradeExtraBallCount { get => extraBallCountLevel; }

    public int ExtraDamageUpgradePrice { get => extraDamageLevel + 1; }
    public int ExtraCritChanceUpgradePrice { get => extraCritChanceLevel + 1; }
    public int ExtraCritDamageUpgradePrice { get => extraCritDamageLevel + 1; }
    public int ExtraBallCountUpgradePrice { get => extraBallCountLevel + 1; }

    // Total stats
    public int TotalDamage { get => damage + extraDamage; }
    public float TotalCritChance { get => critChance + extraCritChance; }
    public float TotalCritDamage { get => critDamage + extraCritDamage; }
    public int TotalBallCount { get => ballCount + extraBallCount; }

    public void AddStats() {
        damage += UpgradeDamage;
        critChance += UpgradeCritChance;
        critDamage += UpgradeCritDamage;
        ballCount += UpgradeBallCount;
        level++;
    }

    public void AddExtraDamage() {
        extraDamage += UpgradeExtraDamage;
        extraDamageLevel++;
    }

    public void AddExtraCritChance() {
        extraCritChance += UpgradeExtraCritChance;
        extraCritChanceLevel++;
    }

    public void AddExtraCritDamage() {
        extraCritDamage += UpgradeExtraCritDamage;
        extraCritDamageLevel++;
    }

    public void AddExtraBallCount() {
        extraBallCount += UpgradeExtraBallCount;
        extraBallCountLevel++;
    }

    public void ResetStats() {
        level = 1;
        damage = 1;
        critChance = 0f;
        critDamage = 2f;
        ballCount = 1;
    }

    public int ModifiedDamage() {
        float damage = TotalDamage;

        float r = UnityEngine.Random.Range(0f, 100f);
        if (r < TotalCritChance)
            damage *= TotalCritDamage;

        return (int)damage;
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
