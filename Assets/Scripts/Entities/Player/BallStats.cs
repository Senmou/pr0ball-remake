using UnityEngine;

public enum BallType { BLUE, GREEN, ORANGE }

[System.Serializable]
public class BallStats {

    public BallType ballType;
    public BallPhysics ballPhysics;
    public float spawnChance;

    [System.Serializable]
    public class BallPhysics {
        public float drag = 0.1f;
        public float gravityScale = 4f;
        public float bounciness = 0.8f;
    }

    public int baseDamage = 1;
    public float critChance = 1f;
    public float critDamageMultiplier = 2.5f;

    public int Damage() {
        float damage = baseDamage;

        float r = Random.Range(0f, 100f);
        if (r < critChance)
            damage *= critDamageMultiplier;

        return (int)damage;
    }

    public void Apply(Ball ball) {
        ball.body.drag = ballPhysics.drag;
        ball.body.gravityScale = ballPhysics.gravityScale;
        ball.body.sharedMaterial.bounciness = ballPhysics.bounciness;
    }
}
