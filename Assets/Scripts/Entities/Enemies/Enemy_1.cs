public class Enemy_1 : BaseEnemy {

    private void OnEnable() {
        maxHP = HP(3, 1) + HP(7, 2) + HP(35, 5);
    }
}
