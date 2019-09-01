using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

    [System.Serializable]
    public class Setups {
        public List<Transform> transforms;
    }

    public static SpawnPoints instance;
    public List<Setups> spawnPoints;
    public List<Setups> bossSpawnPoints;
    public Setups initialSpawnPoints;

    private static int index = 0;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
    }

    private void OnReachedNextLevel() {
        index = 0;
    }

    public List<Transform> GetRandomSpawnPoints() {
        index++;
        if (index >= spawnPoints.Count)
            index = 0;
        return spawnPoints[index].transforms;
    }

    public List<Transform> GetInitialSpawnPoints() {
        return initialSpawnPoints.transforms;
    }

    public List<Transform> GetRandomBossSpawnPoints() {
        int index = Random.Range(0, bossSpawnPoints.Count);
        return bossSpawnPoints[index].transforms;
    }
}
