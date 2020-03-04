public class Enemy_1 : BaseEnemy {

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_1;
    }
}
