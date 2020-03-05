using System.Collections.Generic;
using System.Collections;
using MarchingBytes;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private FloatingText floatingText;
    [SerializeField] private GameObject itemAddSkillPoint;

    public LootDropTable enemyLDT;

    [HideInInspector] public List<BaseEnemy> activeEnemies;
    [HideInInspector] public List<Item_Skillpoint> activeItems;
    [HideInInspector] public PlayStateController playStateController;

    private Canvas canvas;
    private Transform deadline;
    private Transform dottedLine;
    private Transform itemSkillPointContainer;

    private void OnValidate() {
        enemyLDT.ValidateTable();
    }

    private void Awake() {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        activeEnemies = new List<BaseEnemy>();
        deadline = GameObject.Find("Deadline").transform;
        dottedLine = GameObject.Find("DottedLine").transform;
        itemSkillPointContainer = GameObject.Find("ItemSkillPointContainer").transform;
        playStateController = FindObjectOfType<PlayStateController>();

        enemyLDT.ValidateTable();

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void OnSaveGame() {

        PersistentData.instance.currentLevelData.activeEntities.Clear();

        foreach (var entity in activeEnemies) {
            PersistentData.instance.currentLevelData.AddEntity(entity.entityType, entity.transform.position.x, entity.transform.position.y, entity.currentHP);
        }

        foreach (var entity in activeItems) {
            PersistentData.instance.currentLevelData.AddEntity(entity.entityType, entity.transform.position.x, entity.transform.position.y);
        }
    }

    public void LoadEntities() {

        if (PersistentData.instance.currentLevelData.activeEntities != null && PersistentData.instance.currentLevelData.activeEntities.Count > 0) {

            DespawnAllEntities();

            List<CurrentLevelData.EntityData> data = PersistentData.instance.currentLevelData.activeEntities;

            foreach (CurrentLevelData.EntityData item in data) {
                if (item.entityType == CurrentLevelData.EntityType.Item)
                    SpawnItem(new Vector3(item.posX, item.posY));
                else {
                    SpawnEnemy(new Vector3(item.posX, item.posY), item.entityType, item.currentHP);
                }
            }

            PersistentData.instance.currentLevelData.activeEntities.Clear();

        } else {
            // No saved entites found
            CreateInitialWaves();
        }
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

        List<Item_Skillpoint> itemsToRemove = new List<Item_Skillpoint>();
        foreach (var item in activeItems) {
            if (item.transform.position.y >= deadline.position.y) {
                itemsToRemove.Add(item);
            }
        }

        foreach (var item in itemsToRemove) {
            Destroy(item);
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

    public void DespawnAllEntities() {
        foreach (var enemy in activeEnemies) {
            EasyObjectPool.instance.ReturnObjectToPool(enemy.gameObject);
        }
        foreach (var item in activeItems) {
            Destroy(item.gameObject);
        }
        activeItems.Clear();
        activeEnemies.Clear();
    }

    public void CreateWave() {
        MoveEnemiesAndItems();
        List<Transform> spawnPoints = SpawnPoints.instance.GetNextSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {

            int random = Random.Range(1, 100);

            if (random <= 3) {
                SpawnItem(spawnPoints[i].position);
            } else {
                SpawnEnemy(spawnPoints[i].position);
            }
        }
    }

    private void SpawnItem(Vector3 position, bool isInitialWave = false) {
        Item_Skillpoint item = Instantiate(itemAddSkillPoint, position, Quaternion.identity, canvas.transform).GetComponent<Item_Skillpoint>();
        item.transform.SetParent(itemSkillPointContainer);
        activeItems.Add(item);

        if (isInitialWave)
            MoveToStartPosition(item.transform);
    }

    private void SpawnEnemy(Vector3 position, CurrentLevelData.EntityType? entityType = null, int? hp = null, bool isInitialWave = false) {

        string sourcePool = "";
        if (entityType == null)
            sourcePool = enemyLDT.PickLootDropItem().poolName;
        else {
            switch (entityType) {
                case CurrentLevelData.EntityType.Enemy_0:
                    sourcePool = "Enemy_0_pool";
                    break;
                case CurrentLevelData.EntityType.Enemy_1:
                    sourcePool = "Enemy_1_pool";
                    break;
                case CurrentLevelData.EntityType.Enemy_2:
                    sourcePool = "Enemy_2_pool";
                    break;
            }
        }

        BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, position, Quaternion.identity).GetComponent<BaseEnemy>();
        newEnemy.SetData(hp);
        activeEnemies.Add(newEnemy);

        if (isInitialWave)
            MoveToStartPosition(newEnemy.transform);
    }

    public void CreateInitialWaves() {
        StartCoroutine(CreateInitalWavesDelayed());
    }

    private IEnumerator CreateInitalWavesDelayed() {
        yield return new WaitForEndOfFrame();
        List<Transform> spawnPoints = SpawnPoints.instance.GetInitialSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {

            int random = Random.Range(1, 100);

            if (random <= 7) {
                SpawnItem(spawnPoints[i].position, isInitialWave: true);
            } else {
                SpawnEnemy(spawnPoints[i].position, isInitialWave: true);
            }
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

    private void MoveToStartPosition(Transform entity) {
        StartCoroutine(MoveEnemyToStartPosition(entity));
    }

    private IEnumerator MoveEnemyToStartPosition(Transform entity) {

        Vector2 startPos = entity.position;
        Vector2 endPos = startPos + new Vector2(0f, 30f);
        float t = 0;
        float duration = Random.Range(0.4f, 1.2f);
        while (t < 1f) {
            entity.position = Vector2.Lerp(entity.position, endPos, t);
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
