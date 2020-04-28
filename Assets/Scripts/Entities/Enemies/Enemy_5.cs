public class Enemy_5 : BaseEnemy {

    private void OnEnable() {
        maxHP = (int)(hp.MaxHP * GetRemoteHealthMultiplier().enemy_5);
    }

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_5;
    }
}
