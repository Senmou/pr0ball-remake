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

        foreach (var enemy in activeEnemies) {
            PersistentData.instance.currentLevelData.AddEntity(enemy.entityType, enemy.transform.position.x, enemy.transform.position.y, enemy.currentHP);
        }

        foreach (var item in activeItems) {
            PersistentData.instance.currentLevelData.AddEntity(item.entityType, item.transform.position.x, item.transform.position.y, item.value);
        }
    }

    public void LoadEntities() {

        if (PersistentData.instance.currentLevelData.activeEntities != null && PersistentData.instance.currentLevelData.activeEntities.Count > 0) {

            DespawnAllEntities();

            List<CurrentLevelData.EntityData> data = PersistentData.instance.currentLevelData.activeEntities;

            foreach (CurrentLevelData.EntityData entity in data) {
                if (entity.entityType == CurrentLevelData.EntityType.Item)
                    SpawnItem(new Vector3(entity.posX, entity.posY), entity.value);
                else {
                    SpawnEnemy(new Vector3(entity.posX, entity.posY), entity.entityType, entity.value);
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
        for (int i = 0; i < activeEnemies.Count; i++) {
            if (activeEnemies[i].transform.position.y >= deadline.position.y) {

                int inflictedDamage = activeEnemies[i].currentHP * 10;

                activeEnemies[i].Kill();
                Score.instance.DecScore(inflictedDamage);

                // Spawn floating text
                GameObject go = Instantiate(floatingText, activeEnemies[i].transform.position, Quaternion.identity).gameObject;
                go.GetComponent<FloatingText>().SetText("-" + inflictedDamage.ToString());
                go.transform.SetParent(canvas.transform);

                enemiesToRemove.Add(activeEnemies[i]);
            }
        }

        List<Item_Skillpoint> itemsToRemove = new List<Item_Skillpoint>();
        for (int i = 0; i < activeItems.Count; i++) {
            if (activeItems[i].transform.position.y >= deadline.position.y) {
                Destroy(activeItems[i].gameObject);
                itemsToRemove.Add(activeItems[i]);
            }
        }

        for (int i = 0; i < enemiesToRemove.Count; i++) {
            activeEnemies.Remove(enemiesToRemove[i]);
        }

        for (int i = 0; i < itemsToRemove.Count; i++) {
            activeItems.Remove(activeItems[i]);
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

        for (int i = 0; i < activeItems.Count; i++) {
            Destroy(activeItems[i].gameObject);
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

    private void SpawnItem(Vector3 position, int value = -1, bool isInitialWave = false) {
        Item_Skillpoint item = Instantiate(itemAddSkillPoint, position, Quaternion.identity, canvas.transform).GetComponent<Item_Skillpoint>();
        item.transform.SetParent(itemSkillPointContainer);
        if (value != -1)
            item.SetValue(value);
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

        Statistics.Instance.enemies.spawned++;

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
