﻿public class Enemy_2 : BaseEnemy {

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.Enemy_2;
    }
}
