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

    public void UpdateDotColor(int benis) {

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
        if (benis > 5000) {
            index = 6;
        }
        if (benis > 10000) {
            index = 7;
        }

        image.color = accountInfoList[index].dotColor;
    }
}
