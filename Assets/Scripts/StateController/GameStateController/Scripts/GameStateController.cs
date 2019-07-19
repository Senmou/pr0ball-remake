using UnityEngine;

public class GameStateController : StateController {

    public LevelScale levelScale;
    public BallController ballController;

    [HideInInspector] public bool cycleFinished;
}
