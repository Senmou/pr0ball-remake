using System.Collections.Generic;
using System.Collections;
using MarchingBytes;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private FloatingText floatingText;
    [SerializeField] private GameObject itemAddBall;
    [SerializeField] private GameObject itemAddSkillPoint;

    public LootDropTable enemyLDT;

    [HideInInspector] public List<BaseEnemy> activeEnemies;
    [HideInInspector] public List<GameObject> activeItems;
    [HideInInspector] public PlayStateController playStateController;

    private Canvas canvas;
    private Transform deadline;
    private Transform dottedLine;

    private void OnValidate() {
        enemyLDT.ValidateTable();
    }

    private void Awake() {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        activeEnemies = new List<BaseEnemy>();
        deadline = GameObject.Find("Deadline").transform;
        dottedLine = GameObject.Find("DottedLine").transform;
        playStateController = FindObjectOfType<PlayStateController>();

        enemyLDT.ValidateTable();
    }

    public void CheckForEnemiesWhichReachedDeadline() {

        List<BaseEnemy> enemiesToRemove = new List<BaseEnemy>();
        foreach (var enemy in activeEnemies) {
            if (enemy.transform.position.y >= deadline.position.y) {

                int inflictedDamage = enemy.currentHP * 10;

                enemiesToRemove.Add(enemy);
                Score.instance.DecScore(inflictedDamage);

                // Spawn floating text
                GameObject go = Instantiate(floatingText, enemy.transform.position, Quaternion.identity).gameObject;
                go.GetComponent<FloatingText>().SetText("-" + inflictedDamage.ToString());
                go.transform.SetParent(canvas.transform);

                EasyObjectPool.instance.ReturnObjectToPool(enemy.gameObject);
            }
        }

        foreach (var enemyToRemove in enemiesToRemove) {
            enemyToRemove.Kill();
        }
    }

    public bool AllEnemiesBelowDottedLine() {
        foreach (var enemy in activeEnemies) {
            if (enemy.transform.position.y >= dottedLine.position.y)
                return false;
        }
        return true;
    }

    public void DespawnAllEnemies() {
        foreach (var enemy in activeEnemies) {
            EasyObjectPool.instance.ReturnObjectToPool(enemy.gameObject);
        }
        activeEnemies.Clear();
    }

    public void CreateWave() {
        MoveEnemiesAndItems();
        List<Transform> spawnPoints = SpawnPoints.instance.GetNextSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {

            int random = Random.Range(1, 100);

            // Spawn item
            if (random < 7) {
                random = Random.Range(1, 100);

                // Decide which item to spawn
                if (random > 70) {
                    GameObject item = Instantiate(itemAddBall, spawnPoints[i].position, Quaternion.identity, canvas.transform);
                    activeItems.Add(item);
                } else {
                    GameObject item = Instantiate(itemAddSkillPoint, spawnPoints[i].position, Quaternion.identity, canvas.transform);
                    activeItems.Add(item);
                }
            } else {
                // Spawn enemy
                string sourcePool = enemyLDT.PickLootDropItem().poolName;
                BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
                newEnemy.SetData();
                activeEnemies.Add(newEnemy);
            }
        }
    }

    public void CreateInitialWaves() {
        StartCoroutine(CreateInitalWavesDelayed());
    }

    private IEnumerator CreateInitalWavesDelayed() {
        yield return new WaitForEndOfFrame();
        List<Transform> spawnPoints = SpawnPoints.instance.GetInitialSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {
            string sourcePool = enemyLDT.PickLootDropItem().poolName;
            BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, spawnPoints[i].position, Quaternion.identity).GetComponent<BaseEnemy>();
            newEnemy.SetData();
            activeEnemies.Add(newEnemy);
            MoveEnemy(newEnemy);
        }
    }

    private void MoveEnemiesAndItems() {

        Vector3 moveY = new Vector3(0f, 2f);

        foreach (var enemy in activeEnemies) {
            enemy.transform.position += moveY;
        }

        foreach (var item in activeItems) {
            item.transform.position += moveY;
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

    public Vector2 GetRandomTarget() {

        if (activeEnemies.Count == 0)
            return Vector2.zero;

        return activeEnemies.Random().transform.position;
    }
}
