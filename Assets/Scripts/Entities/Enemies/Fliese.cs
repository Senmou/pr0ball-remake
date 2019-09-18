public class Fliese : BaseEnemy {

    private void OnEnable() {
        maxHP = 1 * LevelData.level;
    }
}
