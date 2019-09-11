using UnityEngine;

public class PlayStateController : StateController {

    public LevelScale levelScale;
    public BallController ballController;
    public WaveStateController waveStateController;

    [HideInInspector] public int enemyCount;
    [HideInInspector] public bool hasGameStarted;
    [HideInInspector] public bool cycleFinished;

    private void Awake() {
        waveStateController = FindObjectOfType<WaveStateController>();
    }
}
