using MarchingBytes;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    private const string poolName = "EnemyPool";

    private void Awake() {
        EventManager.StartListening("WaveCompleted", CreateWave);
    }

    public void CreateWave() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetRandomSpawnpoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            GameObject go = EasyObjectPool.instance.GetObjectFromPool(poolName, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
