using System.Collections.Generic;
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
        if (percentage >= 25) {
            index = 1;
        }
        if (percentage > 50) {
            index = 2;
        }
        if (percentage > 100) {
            index = 3;
        }
        if (percentage > 200) {
            index = 4;
        }
        if (percentage > 300) {
            index = 5;
        }
        if (percentage > 400) {
            index = 6;
        }
        if (percentage > 500) {
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
        if (benis > 500) {
            index = 2;
        }
        if (benis > 1000) {
            index = 3;
        }
        if (benis > 2000) {
            index = 4;
        }
        if (benis > 5000) {
            index = 5;
        }
        if (benis > 15000) {
            index = 6;
        }
        if (benis > 20000) {
            index = 7;
        }

        image.color = accountInfoList[index].dotColor;
    }
}
