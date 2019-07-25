using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public BaseEnemy enemy;
    public List<GameObject> setups;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            List<Transform> spawnPoints = SpawnPoints.instance.GetRandomSpawnpoints();

            foreach (var item in spawnPoints) {
                Instantiate(enemy, item.position, Quaternion.identity);
            }
        }
    }

    
}
