using MarchingBytes;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    private const string poolName = "EnemyPool";

    public BaseEnemy enemy;
    public List<GameObject> setups;

    private void Awake() {
        EventManager.StartListening("WaveCompleted", CreateWave);
    }

    private void CreateWave() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetRandomSpawnpoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            GameObject go = EasyObjectPool.instance.GetObjectFromPool(poolName, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
