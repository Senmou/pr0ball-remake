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

    public int level;
    public float spawnChance;

    /**
     * Upgrades purchasable with golden points
     **/
    public int extraDamageLevel;
    public int extraBallCountLevel;
    public int extraCritChanceLevel;
    public int extraCritDamageLevel;

    // Damage
    public int CalcExtraDamage() => extraDamageLevel * 100;
    public int CalcUpgradePriceExtraDamage() => extraDamageLevel + 1;

    // Ball count
    public int CalcExtraBallCount() => extraBallCountLevel;
    public int CalcUpgradePriceExtraBallCount() => extraBallCountLevel + 1;

    // Crit chance
    public float CalcExtraCritChance() => extraCritChanceLevel;
    public int CalcUpgradePriceExtraCritChance() => extraCritChanceLevel + 1;

    // Crit damage
    public float CalcExtraCritDamage() => 0.25f * extraCritDamageLevel;
    public int CalcUpgradePriceExtraCritDamage() => extraCritDamageLevel + 1;

    /**
     * Current ball stats depending on the current upgrade level
     **/
    public int Quantity { get => CalcQuantity(level); }
    public int BaseDamage { get => CalcBaseDamage(level); }
    public int UpgradePrice { get => CalcUpgradePrice(level); }
    public float CritChance { get => CalcCritChance(level); }
    public float CritDamageModifier { get => CalcCritDamageModifier(level); }

    /**
     * Preview for upgraded ball stats
     **/
    public int NextQuantity { get => CalcQuantity(level + 1); }
    public int NextBaseDamage { get => CalcBaseDamage(level + 1); }
    public float NextCritChance { get => CalcCritChance(level + 1); }
    public float NextCritDamageModifier { get => CalcCritDamageModifier(level + 1); }

    /**
     * Ball stats' formulars
     **/
    private int CalcQuantity(int level) => level + CalcExtraBallCount();
    private int CalcUpgradePrice(int level) => level;
    private int CalcBaseDamage(int level) => (int)(level * 2.3f) + CalcExtraDamage();
    private float CalcCritChance(int level) => level * 2f + CalcExtraCritChance();
    private float CalcCritDamageModifier(int level) => 2f + (level - 1) * 0.05f + CalcExtraCritDamage();

    public int ModifiedDamage() {
        float damage = BaseDamage;

        float r = Random.Range(0f, 100f);
        if (r < CritChance)
            damage *= CritDamageModifier;

        return (int)damage;
    }
}
