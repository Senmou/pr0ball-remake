public class Fliese : BaseEnemy {

    private void OnEnable() {
        maxHP = HP(1, 1) + HP(20, 2) + HP(100, 5) + HP(maxHP, 7);
    }
}
