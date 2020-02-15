using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class AccountNameController : MonoBehaviour {

    [SerializeField] private List<AccountInfo> accountInfoList;

    private Image dot;
    private TextMeshProUGUI accountNameUI;

    [Serializable]
    public struct AccountInfo {
        public string accountName;
        public Color dotColor;
    }

    private void Awake() {
        dot = transform.FindChild<Image>("Name/Dot");
        accountNameUI = transform.FindChild<TextMeshProUGUI>("Name");
    }

    private void Update() {
        string accountName = "undefined";
        Color dotColor = Color.white;
        int benis = Score.instance.score;

        int index = 1;
        if (benis < 0) {
            index = 0;
        }  if (benis >= 0) {

            index = 1;
        }  if (benis > 200) {

            index = 2;
        }  if (benis > 500) {

            index = 3;
        }  if (benis > 1000) {

            index = 4;
        }  if (benis > 2500) {

            index = 5;
        }  if (benis > 5000) {

            index = 6;
        }  if (benis > 10000) {

            index = 7;
        }

        accountNameUI.text = accountInfoList[index].accountName;
        dot.color = accountInfoList[index].dotColor;

        if (Input.GetKey(KeyCode.UpArrow)) {
            Score.instance.IncScore(10);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            Score.instance.IncScore(-10);
        }
    }
}
