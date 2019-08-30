
public class NewFag : BaseEnemy {

    private void OnEnable() {
        maxHP = 2 * levelController.currentLevel;
    }
}
