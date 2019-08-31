using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private const string poolName = "EnemyPool";

    public LootDropTable enemyLDT;
    public List<BaseEnemy> activeEnemies;

    private PlayerHP playerHP;
    private bool isBossSpawned;
    private PlayStateController playStateController;

    private void OnValidate() {
        enemyLDT.ValidateTable();
    }

    private void Awake() {
        isBossSpawned = false;
        playerHP = FindObjectOfType<PlayerHP>();
        playStateController = FindObjectOfType<PlayStateController>();
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
        EventManager.StartListening("ReachedBossLevel", OnReachedBossLevel);
        activeEnemies = new List<BaseEnemy>();
        enemyLDT.ValidateTable();
    }

    private void Update() {
        playStateController.enemyCount = activeEnemies.Count;
    }

    private void OnReachedBossLevel() {
        DespawnAllEnemies();
        CreateBossWave();
        isBossSpawned = true;
    }

    private void OnReachedNextLevel() {
        if (isBossSpawned) {
            isBossSpawned = false;
            int remainingEnemies = activeEnemies.Count;
            DespawnAllEnemies();
            playerHP.TakeDamage(remainingEnemies);
        }
    }

    public void OnWaveCompleted() {
        if (!isBossSpawned)
            StartCoroutine(CreateWaveDelayed());
    }

    public void DespawnAllEnemies() {
        foreach (var enemy in activeEnemies) {
            EasyObjectPool.instance.ReturnObjectToPool(enemy.gameObject);
        }
        activeEnemies.Clear();
    }

    public void CreateWave() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
            activeEnemies.Add(newEnemy);
        }
    }

    private void CreateBossWave() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetRandomBossSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
            activeEnemies.Add(newEnemy);
        }
    }

    // Create wave after the other enemies moved up
    private IEnumerator CreateWaveDelayed() {
        yield return new WaitForEndOfFrame();
        CreateWave();
    }

    public Vector2 GetRandomTarget() {

        if (activeEnemies.Count == 0)
            return Vector2.zero;

        return activeEnemies.Random().transform.position;
    }
}
