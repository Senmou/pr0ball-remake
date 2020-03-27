using System.Collections.Generic;
using System.Collections;
using MarchingBytes;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private FloatingText floatingText;

    public LootDropTable itemLDT;
    public LootDropTable enemyLDT;

    [HideInInspector] public List<BaseEnemy> activeEnemies;
    [HideInInspector] public List<BaseItem> activeItems;
    [HideInInspector] public PlayStateController playStateController;

    private Canvas canvas;
    private Transform deadline;
    private Transform dottedLine;
    private Transform itemSkillPointContainer;

    private void OnValidate() {
        itemLDT.ValidateTable();
        enemyLDT.ValidateTable();
    }

    private void Awake() {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        activeEnemies = new List<BaseEnemy>();
        deadline = GameObject.Find("Deadline").transform;
        dottedLine = GameObject.Find("DottedLine").transform;
        itemSkillPointContainer = GameObject.Find("ItemSkillPointContainer").transform;
        playStateController = FindObjectOfType<PlayStateController>();

        EventManager.StartListening("ChacheData", OnChacheData);

        itemLDT.ValidateTable();
        enemyLDT.ValidateTable();
    }

    private bool AllEntitiesReachedStartingPosition() {

        int limit = activeEnemies.Count;
        for (int i = 0; i < limit; i++) {
            if (!activeEnemies[i].reachedStartingPosition)
                return false;
        }

        limit = activeItems.Count;
        for (int i = 0; i < limit; i++) {
            if (!activeItems[i].reachedStartingPosition)
                return false;
        }

        return true;
    }

    private void OnChacheData() {
        StartCoroutine(ChacheEntities());
    }

    private IEnumerator ChacheEntities() {

        yield return new WaitForEndOfFrame();

        while (!AllEntitiesReachedStartingPosition()) {
            yield return null;
        }

        PersistentData.instance.currentLevelData.activeEntities.Clear();

        int limit = activeEnemies.Count;
        for (int i = 0; i < limit; i++) {
            PersistentData.instance.currentLevelData.AddEntity(activeEnemies[i].entityType, activeEnemies[i].transform.position.x, activeEnemies[i].transform.position.y, activeEnemies[i].currentHP);
        }

        limit = activeItems.Count;
        for (int i = 0; i < limit; i++) {
            PersistentData.instance.currentLevelData.AddEntity(activeItems[i].entityType, activeItems[i].transform.position.x, activeItems[i].transform.position.y, activeItems[i].value);
        }
    }

    public void LoadEntities() {

        if (PersistentData.instance.currentLevelData.activeEntities != null && PersistentData.instance.currentLevelData.activeEntities.Count > 0) {
            DespawnAllEntities();

            List<CurrentLevelData.EntityData> activeEntities = PersistentData.instance.currentLevelData.activeEntities;

            int limit = activeEntities.Count;
            for (int i = 0; i < limit; i++) {

                switch (activeEntities[i].entityType) {
                    case CurrentLevelData.EntityType.ItemSkillPoint:
                        SpawnItem(itemLDT.lootDropItems[0].item, new Vector3(activeEntities[i].posX, activeEntities[i].posY), activeEntities[i].value);
                        break;
                    case CurrentLevelData.EntityType.ItemTokenSkill_1:
                        SpawnItem(itemLDT.lootDropItems[1].item, new Vector3(activeEntities[i].posX, activeEntities[i].posY), activeEntities[i].value);
                        break;
                    case CurrentLevelData.EntityType.ItemTokenSkill_2:
                        SpawnItem(itemLDT.lootDropItems[2].item, new Vector3(activeEntities[i].posX, activeEntities[i].posY), activeEntities[i].value);
                        break;
                    case CurrentLevelData.EntityType.ItemTokenSkill_3:
                        SpawnItem(itemLDT.lootDropItems[3].item, new Vector3(activeEntities[i].posX, activeEntities[i].posY), activeEntities[i].value);
                        break;
                    default:
                        SpawnEnemy(new Vector3(activeEntities[i].posX, activeEntities[i].posY), activeEntities[i].entityType, activeEntities[i].value);
                        break;
                }
            }

            PersistentData.instance.currentLevelData.activeEntities.Clear();

        } else {
            // No saved entites found
            CreateInitialWaves();
        }
    }

    public void CheckForEnemiesWhichReachedDeadline() {

        for (int i = activeEnemies.Count - 1; i >= 0; i--) {
            if (activeEnemies[i].transform.position.y >= deadline.position.y) {

                int inflictedDamage = activeEnemies[i].currentHP * 10;
                Vector2 floatingTextSpawnPos = activeEnemies[i].transform.position;

                activeEnemies[i].Kill(shouldIncScore: false);
                Score.instance.DecScore(inflictedDamage);

                GameController.instance.SpawnFloatingText("-" + inflictedDamage.ToString(), floatingTextSpawnPos);
            }
        }

        for (int i = activeItems.Count - 1; i >= 0; i--) {
            if (activeItems[i].transform.position.y >= deadline.position.y) {
                activeItems[i].DestroyAndRemoveItem();
            }
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

        for (int i = activeItems.Count - 1; i >= 0; i--) {
            if (activeItems[i] != null)
                activeItems[i].DestroyAndRemoveItem();
        }

        activeItems.Clear();
        activeEnemies.Clear();
    }

    public void CreateWave() {
        MoveEnemiesAndItems();
        List<Transform> spawnPoints = SpawnPoints.instance.GetNextSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++) {

            int random = Random.Range(1, 100);

            if (random <= 5) {
                SpawnItem(itemLDT.PickLootDropItem().item, spawnPoints[i].position);
            } else {
                SpawnEnemy(spawnPoints[i].position);
            }
        }
    }

    private void SpawnItem(GameObject itemPrefab, Vector3 position, int value = -1, bool isInitialWave = false) {
        BaseItem item = Instantiate(itemPrefab, position, Quaternion.identity, canvas.transform).GetComponent<BaseItem>();
        item.reachedStartingPosition = false;
        item.transform.SetParent(itemSkillPointContainer);
        if (value != -1)
            item.SetValue(value);
        activeItems.Add(item);

        if (isInitialWave)
            MoveToStartPosition(item.transform);
        else
            item.reachedStartingPosition = true;
    }

    private void SpawnEnemy(Vector3 position, CurrentLevelData.EntityType? entityType = null, int? hp = null, bool isInitialWave = false) {

        string sourcePool = "";
        if (entityType == null) {

            // Probability of easier enemies lowers with higher level
            int levelDependentWeight = LevelData.Level;
            enemyLDT.AddWeight("Enemy_0_pool", -levelDependentWeight);
            enemyLDT.AddWeight("Enemy_1_pool", -levelDependentWeight);
            enemyLDT.AddWeight("Enemy_3_pool", -levelDependentWeight);

            // Probability of the hardest enemies grows with the danger level
            int bonusWeight = LevelData.DangerLevel / 3;
            enemyLDT.AddWeight("Enemy_2_pool", bonusWeight / 2);
            enemyLDT.AddWeight("Enemy_4_pool", bonusWeight);
            enemyLDT.AddWeight("Enemy_5_pool", bonusWeight, validateTable: true);

            sourcePool = enemyLDT.PickLootDropItem().poolName;

            // Reset the probability weight after picking an enemy
            enemyLDT.AddWeight("Enemy_0_pool", levelDependentWeight);
            enemyLDT.AddWeight("Enemy_1_pool", levelDependentWeight);
            enemyLDT.AddWeight("Enemy_3_pool", levelDependentWeight);

            enemyLDT.AddWeight("Enemy_2_pool", -bonusWeight / 2);
            enemyLDT.AddWeight("Enemy_4_pool", -bonusWeight);
            enemyLDT.AddWeight("Enemy_5_pool", -bonusWeight, validateTable: true);
        } else {
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
                case CurrentLevelData.EntityType.Enemy_3:
                    sourcePool = "Enemy_3_pool";
                    break;
                case CurrentLevelData.EntityType.Enemy_4:
                    sourcePool = "Enemy_4_pool";
                    break;
                case CurrentLevelData.EntityType.Enemy_5:
                    sourcePool = "Enemy_5_pool";
                    break;
            }
        }

        BaseEnemy newEnemy = EasyObjectPool.instance.GetObjectFromPool(sourcePool, position, Quaternion.identity).GetComponent<BaseEnemy>();
        newEnemy.SetData(hp);
        newEnemy.reachedStartingPosition = false;
        activeEnemies.Add(newEnemy);

        Statistics.Instance.enemies.spawned++;

        if (isInitialWave)
            MoveToStartPosition(newEnemy.transform);
        else
            newEnemy.reachedStartingPosition = true;
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
                SpawnItem(itemLDT.PickLootDropItem().item, spawnPoints[i].position, isInitialWave: true);
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

        var enemy = entity.GetComponent<BaseEnemy>();
        if (enemy)
            enemy.reachedStartingPosition = true;
        else {
            var item = entity.GetComponent<BaseItem>();
            if (item) {
                item.reachedStartingPosition = true;
            }
        }

        yield return null;
    }

    public Vector2 GetRandomTarget() {

        if (activeEnemies.Count == 0)
            return Vector2.zero;

        return activeEnemies.Random().transform.position;
    }
}
