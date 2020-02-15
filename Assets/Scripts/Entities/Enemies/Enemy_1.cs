public class Enemy_1 : BaseEnemy {

    private void OnEnable() {
        maxHP = HP(3, 1) + HP(3, 2) + HP(8, 5);
    }
}
