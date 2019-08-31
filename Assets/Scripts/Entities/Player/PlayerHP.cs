using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour {

    private int currentHP;
    private TextMeshProUGUI playerHp;

    private void Awake() {
        playerHp = transform.FindChild<TextMeshProUGUI>("Value");

        currentHP = 3;
        UpdateUI();
    }

    public void TakeDamage(int amount) {
        currentHP -= amount;
        UpdateUI();
    }

    private void UpdateUI() {
        playerHp.text = currentHP.ToString();
    }
}
