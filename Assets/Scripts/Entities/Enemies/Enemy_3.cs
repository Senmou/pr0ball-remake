public class Enemy_3 : BaseEnemy {

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_3;
    }
}
