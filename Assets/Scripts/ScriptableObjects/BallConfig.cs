using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Ball/Default")]
public class BallConfig : ScriptableObject {

    public float drag = 0.1f;
    public float gravityScale = 4f;
    public float bounciness = 0.8f;

    public void Apply(Ball ball) {
        ball.body.drag = drag;
        ball.body.gravityScale = gravityScale;
        ball.body.sharedMaterial.bounciness = bounciness;
    }
}
