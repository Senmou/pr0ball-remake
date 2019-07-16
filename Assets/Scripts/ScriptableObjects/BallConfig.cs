using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Ball/Default")]
public class BallConfig : ScriptableObject {

    public float drag = 0.1f;
    public float gravityScale = 4f;
    public bool freezeRotation = true;

    public bool test;
}
