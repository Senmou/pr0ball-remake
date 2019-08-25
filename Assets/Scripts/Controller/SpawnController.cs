using UnityEngine;
using MarchingBytes;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

    public LootDropTable enemyLDT;

    private const string poolName = "EnemyPool";

    private void OnValidate() {
        enemyLDT.ValidateTable();
    }

    private void Awake() {
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
        enemyLDT.ValidateTable();
    }

    public void OnWaveCompleted() {
        StartCoroutine(CreateWaveDelayed());
    }

    public void CreateWave() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetRandomSpawnpoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
        }
    }

    // Create wave after the other enemies moved up
    private IEnumerator CreateWaveDelayed() {
        yield return new WaitForEndOfFrame();
        CreateWave();
    }
}
