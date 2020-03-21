﻿using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DotColorController : MonoBehaviour {

    [SerializeField] private List<AccountInfo> accountInfoList;

    private Image image;

    [Serializable]
    public struct AccountInfo {
        public string accountName;
        public Color dotColor;
    }

    private void Awake() {
        image = GetComponent<Image>();
    }

    public Color GetDangerLevelColor(int percentage) {

        int index = 0;
        if (percentage >= 0) {
            index = 0;
        }
        if (percentage >= 20) {
            index = 1;
        }
        if (percentage > 50) {
            index = 2;
        }
        if (percentage > 120) {
            index = 3;
        }
        if (percentage > 150) {
            index = 4;
        }
        if (percentage > 175) {
            index = 5;
        }
        if (percentage > 200) {
            index = 6;
        }
        if (percentage > 250) {
            index = 7;
        }

        return accountInfoList[index].dotColor;
    }

    public void UpdateDotColor(long benis) {

        int index = 1;
        if (benis < 0) {
            index = 0;
        }
        if (benis >= 0) {
            index = 1;
        }
        if (benis > 200) {
            index = 2;
        }
        if (benis > 500) {
            index = 3;
        }
        if (benis > 1000) {
            index = 4;
        }
        if (benis > 2500) {
            index = 5;
        }
        if (benis > 7500) {
            index = 6;
        }
        if (benis > 15000) {
            index = 7;
        }

        image.color = accountInfoList[index].dotColor;
    }
}
