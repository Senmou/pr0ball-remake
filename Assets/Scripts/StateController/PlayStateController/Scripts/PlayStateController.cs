using UnityEngine;

public class PlayStateController : StateController {

    [HideInInspector] public bool fightingBoss;
    [HideInInspector] public bool cycleFinished;
    [HideInInspector] public bool hasGameStarted;
    [HideInInspector] public bool reachedNextWave;
    [HideInInspector] public bool reachedNextLevel;
    [HideInInspector] public bool reachedBossLevel;
    [HideInInspector] public bool nextLevelIsBoss;

    [HideInInspector] public PlayerHP playerHP;
    [HideInInspector] public LevelScale levelScale;
    [HideInInspector] public BallController ballController;
    [HideInInspector] public EnemyController enemyController;

    private void Awake() {
        playerHP = FindObjectOfType<PlayerHP>();
        enemyController = FindObjectOfType<EnemyController>();
    }
}
