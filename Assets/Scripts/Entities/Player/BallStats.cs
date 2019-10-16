﻿using UnityEngine;

public enum BallColor { BLUE, GREEN, ORANGE }

[System.Serializable]
public class BallStats {

    public BallColor ballColor;
    public BallPhysics ballPhysics;

    public int level;
    public float spawnChance;

    public int extraDamage;
    public int extraBallCount;
    public float extraCritChance;
    public float extraCritDamage;

    public int BaseDamage { get => CalcBaseDamage(level); }
    public float CritChance { get => CalcCritChance(level); }
    public float CritDamageModifier { get => CalcCritDamageModifier(level); }

    public int NextLevel { get => level + 1; }
    public int Quantity { get => CalcQuantity(level); }
    public int UpgradePrice { get => CalcUpgradePrice(level); }
    public int NextBaseDamage { get => CalcBaseDamage(level + 1); }
    public float NextCritChance { get => CalcCritChance(level + 1); }
    public float NextCritDamageModifier { get => CalcCritDamageModifier(level + 1); }

    private int CalcQuantity(int level) => level + extraBallCount;
    private int CalcUpgradePrice(int level) => level;
    private int CalcBaseDamage(int level) => (int)(level * 2.3f) + extraDamage;
    private float CalcCritChance(int level) => level * 2f + extraCritChance;
    private float CalcCritDamageModifier(int level) => 2f + (level - 1) * 0.05f + extraCritDamage;

    public int ModifiedDamage() {
        float damage = BaseDamage;

        float r = Random.Range(0f, 100f);
        if (r < CritChance)
            damage *= CritDamageModifier;

        return (int)damage;
    }

    public void Apply(Ball ball) {
        ball.body.drag = ballPhysics.drag;
        ball.body.gravityScale = ballPhysics.gravityScale;
        ball.body.sharedMaterial.bounciness = ballPhysics.bounciness;
    }

    [System.Serializable]
    public class BallPhysics {
        public float drag = 0.1f;
        public float gravityScale = 4f;
        public float bounciness = 0.8f;
    }
}
