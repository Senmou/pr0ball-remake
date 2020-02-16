﻿using MarchingBytes;
using System.Collections;
using UnityEngine;

public class Skill_D : Skill {

    [SerializeField] private int frogCount;

    private const string frogPoolName = "Frog_pool";

    protected override IEnumerator ActionCoroutine() {

        for (int i = 0; i < frogCount; i++) {
            float posY = 23f;
            float posX = Random.Range(-9.36f, 9.36f);
            Frog frog = EasyObjectPool.instance.GetObjectFromPool(frogPoolName, new Vector3(posX, posY), Quaternion.identity).GetComponent<Frog>();
            yield return new WaitForSeconds(0.3f);
        }

        pending = false;

        yield return null;
    }
}
