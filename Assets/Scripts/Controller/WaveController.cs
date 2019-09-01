using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wave {

    public List<BaseEnemy> enemies;
    public List<Transform> spawnPoints;

    public Wave() {
        spawnPoints = SpawnPoints.instance.GetInitialSpawnPoints();
    }
}

public class WaveController : MonoBehaviour {

    public static WaveController instance;

    public GameObject waveUI;
    public TextMeshProUGUI currentWaveUI;

    public int currentWave;
    public int wavesPerLevel;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        EventManager.StartListening("ReachedNextLevel", ResetWaveCount);
        EventManager.StartListening("ReachedBossLevel", ResetWaveCount);
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
        EventManager.StartListening("FailedLevel", ResetWaveCount);
    }

    private void Start() {
        UpdateWaveUI();
    }

    private void ResetWaveCount() {
        currentWave = 1;
        UpdateWaveUI();
    }

    private void OnWaveCompleted() {
        currentWave++;

        if (currentWave > wavesPerLevel) {
            currentWave = 1;
            EventManager.TriggerEvent("ReachedNextLevel");
        }

        UpdateWaveUI();
    }
    
    private void UpdateWaveUI() {
        currentWaveUI.text = currentWave.ToString() + "/" + wavesPerLevel;
    }
}
