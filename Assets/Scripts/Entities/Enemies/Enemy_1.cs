public class Enemy_1 : BaseEnemy {

    private void OnEnable() {
        maxHP = (int)(hp.MaxHP * GetRemoteHealthMultiplier().enemy_1);
    }

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_1;
    }
}
