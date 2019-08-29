using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public List<BaseEnemy> activeEnemies;

    private void Awake() {
        activeEnemies = new List<BaseEnemy>();
    }

    public Vector2 GetRandomTarget() {

        if (activeEnemies.Count == 0)
            return Vector2.zero;

        return activeEnemies.Random().transform.position;
    }
}
