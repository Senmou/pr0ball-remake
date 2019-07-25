using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

    [System.Serializable]
    public class Setups {
        public List<Transform> transforms;
    }

    public static SpawnPoints instance;
    public List<Setups> spawnPoints;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public List<Transform> GetRandomSpawnpoints() {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index].transforms;
    }
}
