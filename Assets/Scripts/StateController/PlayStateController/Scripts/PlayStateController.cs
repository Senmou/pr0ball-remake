using UnityEngine;

public class PlayStateController : StateController {

    public LevelScale levelScale;
    public BallController ballController;

    [HideInInspector] public bool hasGameStarted;
    [HideInInspector] public bool cycleFinished;
}
