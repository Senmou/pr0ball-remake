public class Enemy_2 : BaseEnemy {

    private void OnEnable() {
        maxHP = (int)(hp.MaxHP * GetRemoteHealthMultiplier().enemy_2);
    }

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_2;
    }
}
