public class Enemy_0 : BaseEnemy {

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_0;
        hpMultiplicator = 1f;
    }
}
