using UnityEngine;

public class PlayStateController : StateController {

    [HideInInspector] public bool cycleFinished;
    [HideInInspector] public bool hasGameStarted;

    [HideInInspector] public LevelScale levelScale;
    [HideInInspector] public BallController ballController;
    [HideInInspector] public EnemyController enemyController;

    private void Awake() {
        enemyController = FindObjectOfType<EnemyController>();
    }
}
