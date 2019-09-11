
public class NewFag : BaseEnemy {

    private void OnEnable() {
        maxHP = 2 * waveStateController.CurrentLevel;
    }
}
