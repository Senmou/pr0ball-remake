using UnityEngine;

public class WaveStateController : StateController {

    [HideInInspector] public EnemyController enemyController;
    [HideInInspector] public bool waveCompleted;
    [HideInInspector] public bool levelCompleted;
    [HideInInspector] public bool bossLevelUpcoming;

    public const int wavesPerLevel = 20;

    private int currentWave;
    private int currentLevel;

    public int CurrentWave {
        get => currentWave;
        set {
            currentWave = value;
            if (currentWave > wavesPerLevel) {
                currentWave = 1;
                currentLevel++;
            }
        }
    }

    public int CurrentLevel { get => currentLevel; }

    private void Awake() {
        currentWave = 1;
        currentLevel = 1;

        enemyController = FindObjectOfType<EnemyController>();
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }

    private void OnWaveCompleted() {

        if (currentWave == wavesPerLevel) {
            if (CurrentLevel % 10 == 9) {
                bossLevelUpcoming = true;
                return;
            }
            levelCompleted = true;
        }
        else
            waveCompleted = true;

        CurrentWave++;
    }
}
