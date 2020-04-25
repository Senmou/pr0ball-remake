public class Enemy_4 : BaseEnemy {

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_4;
        hpMultiplicator = 5f;
    }
}
