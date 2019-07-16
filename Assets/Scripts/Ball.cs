using UnityEngine;

public class Ball : MonoBehaviour {

    public BallConfig ballConfig;

    [HideInInspector] public Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();

        ApplyConfig(ballConfig);
    }

    private void ApplyConfig(BallConfig config) {
        body.drag = config.drag;
        body.gravityScale = config.gravityScale;
        body.freezeRotation = config.freezeRotation;
    }
}
