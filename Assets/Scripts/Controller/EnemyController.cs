using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public LootDropTable enemyLDT;
    public List<BaseEnemy> activeEnemies;

    private const string poolName = "EnemyPool";

    private void OnValidate() {
        enemyLDT.ValidateTable();
    }

    private void Awake() {
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
        activeEnemies = new List<BaseEnemy>();
        enemyLDT.ValidateTable();
    }

    public void OnWaveCompleted() {
        StartCoroutine(CreateWaveDelayed());
    }

    public void CreateWave() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetRandomSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
            activeEnemies.Add(newEnemy);
        }
    }

    public void CreateBossWave() {
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
