using System;
using UnityEngine;

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

    public int UpgradeDamage { get => VES(level, (1, 1)); }
    public float UpgradeCritChance { get => (critChance < 100f) ? 3f : 0f; }
    public float UpgradeCritDamage { get => 0f; }
    public int UpgradeBallCount { get => 0; }

    public int UpgradePrice { get => 2; }

    private BallController ballController;

    public void AddStats() {
        damage += UpgradeDamage;
        critChance += UpgradeCritChance;
        if (critChance > 100f)
            critChance = 100f;
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

    public void OnItemCollected_AddBall() {
        ballCount++;
        ballController = GameObject.FindObjectOfType<BallController>();
        ballController.SetMaxBallCount(ballCount);
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
