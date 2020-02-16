public class Enemy_2 : BaseEnemy {

    private void OnEnable() {
        maxHP = HP(10, 1) + HP(40, 5) + HP(70, 10);
    }
}
