﻿public class Enemy_1 : BaseEnemy {

    private void OnEnable() {
        maxHP = 2 * HP(1, 1) + HP(20, 2) + HP(100, 5);
    }
}