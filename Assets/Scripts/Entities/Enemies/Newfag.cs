using UnityEngine;

public class NewFag : BaseEnemy {

    private void OnEnable() {
        maxHP = Mathf.FloorToInt(2f * levelController.currentLevel);
    }
}
