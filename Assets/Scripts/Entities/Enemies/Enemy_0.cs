public class Enemy_0 : BaseEnemy {

    private void OnEnable() {
        maxHP = HP(1, 1) + HP(5, 2) + HP(20, 5);
    }
}
