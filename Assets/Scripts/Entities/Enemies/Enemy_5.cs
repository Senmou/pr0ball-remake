public class Enemy_5 : BaseEnemy {

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_5;
        hpMultiplicator = RemoteConfig.remoteConfig.healthMultiplier.enemy_5;
    }
}
