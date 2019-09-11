using System.Collections.Generic;
using System.Collections;
using MarchingBytes;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private const string poolName = "EnemyPool";

    public LootDropTable enemyLDT;
    public List<BaseEnemy> activeEnemies;
    
    private PlayStateController playStateController;

    private void OnValidate() {
        enemyLDT.ValidateTable();
    }

    private void Awake() {
        playStateController = FindObjectOfType<PlayStateController>();
       
        activeEnemies = new List<BaseEnemy>();
        enemyLDT.ValidateTable();
    }

    private void Update() {
        playStateController.enemyCount = activeEnemies.Count;
    }
    
    public void DespawnAllEnemies() {
        foreach (var enemy in activeEnemies) {
            EasyObjectPool.instance.ReturnObjectToPool(enemy.gameObject);
        }
        activeEnemies.Clear();
    }

    public void CreateWave() {
        MoveEnemies();
        List<Transform> spawnPoints = SpawnPoints.instance.GetRandomSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
            activeEnemies.Add(newEnemy);
        }
    }

    public void CreateInitialWaves() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetInitialSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
            activeEnemies.Add(newEnemy);
            MoveEnemy(newEnemy);
        }
    }

    private void MoveEnemies() {
        foreach (var enemy in activeEnemies) {
            enemy.transform.position += new Vector3(0f, 2f);
        }
    }

    private void MoveEnemy(BaseEnemy enemy) {
        StartCoroutine(MoveEnemyToStartPosition(enemy));
    }

    private IEnumerator MoveEnemyToStartPosition(BaseEnemy enemy) {

        Vector2 startPos = enemy.transform.position;
        Vector2 endPos = startPos + new Vector2(0f, 30f);
        float t = 0;
        float duration = Random.Range(0.4f, 1.2f);
        while (t < 1f) {
            enemy.transform.position = Vector2.Lerp(enemy.transform.position, endPos, t);
            t += Time.deltaTime / duration;
            yield return null;
        }
        yield return null;
    }

    public void CreateBossWave() {
        List<Transform> spawnPoints = SpawnPoints.instance.GetRandomBossSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
            activeEnemies.Add(newEnemy);
            MoveEnemy(newEnemy);
        }
    }

    public Vector2 GetRandomTarget() {

        if (activeEnemies.Count == 0)
            return Vector2.zero;

        return activeEnemies.Random().transform.position;
    }
}
