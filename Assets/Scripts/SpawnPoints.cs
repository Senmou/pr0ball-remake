using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

    [System.Serializable]
    public class Setups {
        public List<Transform> transforms;
    }

    public static SpawnPoints instance;
    public List<Setups> spawnPoints;
    public Setups initialSpawnPoints;

    private static int index = 0;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        EventManager.StartListening("ChacheData", OnChacheData);
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);

        index = PersistentData.instance.currentLevelData.spawnPointIndex;
    }

    private void OnChacheData() {
        PersistentData.instance.currentLevelData.spawnPointIndex = index;
    }

    private void OnReachedNextLevel() {
        index = 0;
    }

    public List<Transform> GetNextSpawnPoints() {
        index++;
        if (index >= spawnPoints.Count)
            index = 0;
        return spawnPoints[index].transforms;
    }

    public List<Transform> GetInitialSpawnPoints() {
        index = 0;
        return initialSpawnPoints.transforms;
    }
}
