using UnityEngine;

public class PlayStateController : StateController {

    public LevelScale levelScale;
    public BallController ballController;

    [HideInInspector] public int enemyCount;
    [HideInInspector] public bool hasGameStarted;
    [HideInInspector] public bool cycleFinished;
}
