public class Enemy_2 : BaseEnemy {

    private void OnEnable() {
        maxHP = 10 * HP(1, 1) + HP(20, 2) + HP(100, 5);
    }
}
