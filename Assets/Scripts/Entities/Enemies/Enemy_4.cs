public class Enemy_4 : BaseEnemy {

    private void OnEnable() {
        maxHP = (int)(hp.MaxHP * GetRemoteHealthMultiplier().enemy_4);
    }

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_4;
    }
}
