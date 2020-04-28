public class Enemy_3 : BaseEnemy {

    private void OnEnable() {
        maxHP = (int)(hp.MaxHP * GetRemoteHealthMultiplier().enemy_3);
    }

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_3;
    }
}
