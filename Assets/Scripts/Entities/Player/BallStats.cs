using UnityEngine;

public class BallStats : MonoBehaviour {

    public float drag = 0.1f;
    public float gravityScale = 4f;
    public float bounciness = 0.8f;

    private int baseDamage = 50;
    private float critChance = 1f;
    private float critDamageMultiplier = 2.5f;

    public float CritChance { get => critChance; }
    public float CritDamageMultiplier { get => critDamageMultiplier; }

    public int Damage() {
        float damage = baseDamage;

        float r = Random.Range(0f, 100f);
        if (r < critChance) {
            damage *= critDamageMultiplier;
            Debug.Log("CRIT: " + damage);
        }

        return (int)damage;
    }

    public void Apply(Ball ball) {
        ball.body.drag = drag;
        ball.body.gravityScale = gravityScale;
        ball.body.sharedMaterial.bounciness = bounciness;
    }
}
